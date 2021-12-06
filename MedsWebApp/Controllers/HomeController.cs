using MedsWebApp.Models;
using MedsWebApp.Repositories;
using MedsWebApp.Services;
using MedsWebApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using NETCore.MailKit.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static MedsWebApp.Models.Order;
using static MedsWebApp.Models.User;
using static MedsWebApp.Services.SendEmailService;

namespace MedsWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _dbContext;
        private readonly MedicineInPharmacyRepository medicineInPharmacyRepository;
        private readonly MedicineRepository medicineRepository;
        private readonly CategoryRepository categoryRepository;
        private readonly UserRepository userRepository;
        private readonly OrdersRepository orderRepository;
        private readonly OrderItemRepository orderItemsRepository;
        private readonly DiscountRepository discountRepository;
        private const int PAGE_SIZE = 7;
        private readonly IHubContext<OrdersInfoHub> _orderInfoHub;
        private readonly IEmailService mailService;
        private readonly ViewRender _viewRender;
        private readonly PharmacyRepository pharmacyRepository;



        public HomeController(ILogger<HomeController> logger, ApplicationContext context, IHubContext<OrdersInfoHub> hubContext, IEmailService emailService, ViewRender viewRender)
        {
            _logger = logger;
            _dbContext = context;
            medicineInPharmacyRepository = new MedicineInPharmacyRepository(context);
            medicineRepository = new MedicineRepository(context);
            categoryRepository = new CategoryRepository(context);
            userRepository = new UserRepository(context);
            orderRepository = new OrdersRepository(context);
            orderItemsRepository = new OrderItemRepository(context);
            discountRepository = new DiscountRepository(context);
            _orderInfoHub = hubContext;
            mailService = emailService;
            _viewRender = viewRender;
            pharmacyRepository = new PharmacyRepository(context);
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Search(string q, int page = 1)
        {
            var (meds, totalPages) = string.IsNullOrWhiteSpace(q) ?
                (Array.Empty<Medicine>(), 1) :
                await medicineRepository.FindByPartOfName(q, page, PAGE_SIZE);
            ViewBag.SearchString = q;
            if (page > totalPages) return View(Array.Empty<MedicineViewModel>());
            ViewBag.Pages = totalPages;
            ViewBag.Page = page;
            return View(meds.AsViewModel());
        }
        [HttpGet]
        public async Task<IActionResult> Medicine(int id)
        {
            var cartCookie = HttpContext.Session.GetString("cart");
            if (string.IsNullOrEmpty(cartCookie))
                ViewBag.CartList = Array.Empty<CartCookie>();
            else
            {
                var cartCookieObject = JsonConvert.DeserializeObject<CartCookie[]>(cartCookie);
                ViewBag.CartList = cartCookieObject;
            }
            var medicine = await medicineRepository.FindByIdEager(id);
            if (medicine is null) return NotFound();
            ViewBag.MedicineInPharmacies = medicine.MedicinesInPharmacy.AsViewModel();
            return View(medicine.AsViewModel());
        }
        [HttpPost]
        public IActionResult AddToCart(int id, int medicineId, int count, bool fromCart)
        {
            var cartCookie = HttpContext.Session.GetString("cart");
            CartCookie[] idInCart;
            var newItem = new CartCookie { MedicineInPharmacyId = id, Count = count };
            if (string.IsNullOrEmpty(cartCookie))
                idInCart = new CartCookie[] { newItem };
            else
            {
                idInCart = JsonConvert.DeserializeObject<CartCookie[]>(cartCookie);
                var exists = idInCart.FirstOrDefault(cc => cc.MedicineInPharmacyId == id);
                if (exists == null) idInCart = idInCart.Append(newItem).ToArray();
                else exists.Count = count;
            }
            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(idInCart));
            if (fromCart) return RedirectToAction("Cart");
            return RedirectToAction("Medicine", new { id = medicineId });
        }
        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            var cartCookie = HttpContext.Session.GetString("cart");
            CartCookie[] idInCart;
            if (string.IsNullOrEmpty(cartCookie)) return RedirectToAction("Cart");
            idInCart = JsonConvert.DeserializeObject<CartCookie[]>(cartCookie);
            idInCart = idInCart.Where(item => item.MedicineInPharmacyId != id).ToArray();
            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(idInCart));
            return RedirectToAction("Cart");
        }
        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            var cartCookie = HttpContext.Session.GetString("cart");
            CartCookie[] idInCart = null;
            if (string.IsNullOrEmpty(cartCookie)) return View(null);
            idInCart = JsonConvert.DeserializeObject<CartCookie[]>(cartCookie);
            if (idInCart.Length == 0) return View(null);
            var meds = await medicineInPharmacyRepository.FindByIdsEager(
                idInCart.Select(i => i.MedicineInPharmacyId).ToArray()
                );
            var groupedCartItems = meds
                .GroupBy(m => m.Pharmacy)
                .ToDictionary(
                    gr => gr.Key.AsViewModel(),
                    gr => gr
                        .Select(
                            gri => (gri.AsViewModel(), idInCart.First(c => c.MedicineInPharmacyId == gri.Id).Count)));
            return View(groupedCartItems);
        }
        [HttpGet]
        public async Task<IActionResult> Range()
        {
            var root = await categoryRepository.GetRootCategories();
            return View(root.AsViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> LoadCategoryChildren(int id)
        {
            var hasMeds = await medicineRepository.HasMedicinesByCategory(id);
            if (hasMeds)
            {
                var medicines = await medicineRepository.FindByCategory(id);
                return PartialView("_ListOfMeds", medicines.AsViewModel());
            }
            else
            {
                var result = await categoryRepository.GetChildren(id);
                return PartialView("_CategoryItems", result.AsViewModel());
            }

        }
        [HttpGet]
        public async Task<IActionResult> Order()
        {
            var orderIdString = HttpContext.Session.GetString("order");
            if (string.IsNullOrEmpty(orderIdString)) return NotFound();
            var orderIds = JsonConvert.DeserializeObject<int[]>(orderIdString);
            var orders = new List<Order>(orderIds.Length);
            foreach (var orderId in orderIds)
            {
                var order = await orderRepository.LoadEagerNoTracking(orderId);
                orders.Add(order);
            }
            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(string email, string username)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(username) || !new EmailAddressAttribute().IsValid(email)) return BadRequest();
            var cartCookie = HttpContext.Session.GetString("cart");
            CartCookie[] idInCart = null;
            if (string.IsNullOrEmpty(cartCookie)) return NotFound();
            idInCart = JsonConvert.DeserializeObject<CartCookie[]>(cartCookie);
            if (idInCart.Length == 0) return NotFound();
            var meds = await medicineInPharmacyRepository.FindByIdsEager(
                idInCart.Select(i => i.MedicineInPharmacyId).ToArray()
                );
            var user = await userRepository.FindByEmail(email, Models.User.Roles.Customer);
            if (user == null) user = await userRepository.Insert(new User { Email = email, Name = username, Role = Roles.Customer });
            var orderItems = meds
                .Select(med => new OrderItem
                {
                    MedicineInPharmacy = med,
                    Price = med.AsViewModel().DiscountPrice ?? med.Price,
                    Count = (uint)idInCart.First(c => c.MedicineInPharmacyId == med.Id).Count,
                }).ToList();

            foreach (var orderItem in orderItems)
            {
                orderItem.MedicineInPharmacy.AvailableCount = orderItem.MedicineInPharmacy.AvailableCount - (int)orderItem.Count;
            }
            var grouppedOrderItems = orderItems.GroupBy(oi => oi.MedicineInPharmacy.PharmacyId);
            var orderIds = new List<int>();
            var orders = new List<Order>();
            foreach (var grouppedOrderItem in grouppedOrderItems)
            {
                var order = new Order
                {
                    CreateDateTime = DateTime.Now,
                    User = user,
                    OrderItems = grouppedOrderItem.ToList(),
                    Status = OrderStatus.New,
                    Price = grouppedOrderItem.Sum(item => item.Price * item.Count)
                };
                order = await orderRepository.Insert(order);
                await NotifyNewOrder(order.Id);
                orderIds.Add(order.Id);
                var loadedOrder = await orderRepository.LoadEagerNoTracking(order.Id);
                orders.Add(loadedOrder);
            }
            var isMailSended = await mailService.SendOrderInfoEmail(_logger, _viewRender, user.Email, orders);
            HttpContext.Session.Remove("cart");
            HttpContext.Session.SetString("order", JsonConvert.SerializeObject(orderIds));
            return RedirectToAction("Order");
        }
        private async Task NotifyNewOrder(int orderId)
        {
            var order = await orderRepository.LoadEagerNoTracking(orderId);
            var pharmacy =  await pharmacyRepository.FindById(order.OrderItems[0].MedicineInPharmacy.PharmacyId);
            foreach (var user in pharmacy.Users)
            {
                await _orderInfoHub.Clients.Group(user.Email).SendAsync("UpdateOrder", JsonConvert.SerializeObject(order.AsViewModel()));
            }
        }
        [HttpPost]
        public async Task<IActionResult> LoadDiscountMedicines(int id)
        {
            var medicineInPharmacies = await medicineInPharmacyRepository.FindByDiscountIdEager(id);
            if (medicineInPharmacies.Length == 0) return PartialView("_ListOfMedicineInPharmacy", null);
            var medicines = medicineInPharmacies.Select(mip => mip.Medicine).Distinct().ToArray();
            return PartialView("_ListOfMeds", medicines.AsViewModel());
        }
        [HttpGet]
        public async Task<IActionResult> Stocks()
        {
            var discounts = await discountRepository.GetActual();
            return View(discounts.AsViewModel());
        }
        public IActionResult Services()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contacts()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ApiLogin()
        {
            ViewData["TargetController"] = "Home";
            ViewData["TargetAction"] = "ApiLogin";
            ViewData["TargetRegistrationAction"] = "ApiRegistration";
            return View("Login");
        }
        [HttpPost]
        public async Task<IActionResult> ApiLogin(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || !new EmailAddressAttribute().IsValid(email)) return BadRequest();
            var user = await userRepository.Login(email, password, Models.User.Roles.ApiUser);
            ViewData["TargetController"] = "Home";
            ViewData["TargetAction"] = "ApiLogin";
            ViewData["TargetRegistrationAction"] = "ApiRegistration";
            if (user != null)
            {
                if (!user.IsEmailConfirmed) return View("Login", (email, true, false, user.Role));
                HttpContext.Session.SetInt32("userId", user.Id);
                return RedirectToActionPermanent("ApiAccount");
            }
            return View("Login", (email, false, false, Models.User.Roles.None));
        }
        [HttpGet]
        public IActionResult ApiRegistration()
        {
            ViewData["TargetController"] = "Home";
            ViewData["TargetAction"] = "ApiRegistration";
            ViewData["TargetLoginAction"] = "ApiLogin";
            return View("Registration");
        }
        [HttpPost]
        public async Task<IActionResult> ApiRegistration(string email, string password, string name)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || !new EmailAddressAttribute().IsValid(email)) return BadRequest();
            var user = await userRepository.FindByEmail(email, Models.User.Roles.ApiUser);
            ViewData["TargetController"] = "Home";
            ViewData["TargetAction"] = "ApiRegistration";
            ViewData["TargetLoginAction"] = "ApiLogin";
            if (user != null) return View("Registration", (email, false, false));
            user = await userRepository.Register(email, password, Models.User.Roles.ApiUser, name, null);
            var isEmailSended = await mailService.SendRegistrationApiUserEmail(_logger, _viewRender, user, $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}");
            if (!isEmailSended) return View("Registration", (email, false, false));
            return View("Registration", (email, true, true));
        }
        [HttpPost]
        public async Task<IActionResult> ResendRegistrationEmail(string email, Roles role)
        {
            if (string.IsNullOrWhiteSpace(email) || role == Roles.None || !new EmailAddressAttribute().IsValid(email)) return BadRequest();
            var user = await userRepository.FindByEmail(email, role);
            if (user == null || user.IsEmailConfirmed) return BadRequest();
            if (await mailService.SendRegistrationApiUserEmail(_logger, _viewRender, user, $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}")) return Ok();
            return Error();
        }
        [HttpGet]
        public async Task<IActionResult> ApiAccount()
        {
            var userId = HttpContext.Session.GetInt32("userId");
            if (!userId.HasValue) return NotFound();
            var user = await userRepository.FindById(userId.Value, Models.User.Roles.ApiUser);
            if (user == null) return NotFound();
            var (accessToken, expire) = AuthOptions.GenerateJWT(user);
            return View((accessToken, expire, user));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRefreshToken()
        {
            var userId = HttpContext.Session.GetInt32("userId");
            if (!userId.HasValue) return NotFound();
            var user = await userRepository.FindById(userId.Value, Models.User.Roles.ApiUser);
            if (user == null) return NotFound();
            var (refreshToken, expire) = AuthOptions.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpire = expire;
            await userRepository.Update(user);
            return RedirectToAction("ApiAccount");
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string access_token)
        {
            if (string.IsNullOrWhiteSpace(access_token)) return BadRequest();
            var claims = AuthOptions.ValidateToken(access_token);
            var email = claims?.Identity?.Name;
            if (string.IsNullOrEmpty(email)) return BadRequest();
            var roleValue = claims.FindFirst(ClaimsIdentity.DefaultRoleClaimType)?.Value;
            if (string.IsNullOrEmpty(roleValue)) return BadRequest();
            var role = GetRoleFromString(roleValue);
            if (role == Roles.None) return BadRequest();
            var user = await userRepository.FindByEmail(email, role);
            if (user == null) return BadRequest();
            if (user.IsEmailConfirmed) return NotFound();
            user.IsEmailConfirmed = true;
            user = await userRepository.Update(user);
            (string loginController, string loginAction) linkData = user.Role switch
            {
                Roles.None => throw new NotImplementedException(),
                Roles.Admin => ("Pharmacy", "Login"),
                Roles.ApiUser => ("Home", "ApiLogin"),
                Roles.Customer => throw new NotImplementedException(),
                Roles.PharmacyUser => ("Pharmacy", "Login"),
                _ => throw new NotImplementedException(),
            };
            return View(linkData);
        }
        [HttpGet("[action]")]
        public IActionResult ResetPasswordRequest(Roles role)
        {
            if (role == Roles.None) return BadRequest();
            return View((role, string.Empty, false));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> ResetPasswordRequest(string email, Roles role)
        {
            if (string.IsNullOrWhiteSpace(email) || role == Roles.None || !new EmailAddressAttribute().IsValid(email)) return BadRequest();
            var user = await userRepository.FindByEmail(email, role);
            if (user == null) return View((role, email, false));
            var isMailSended = await mailService.SendResetPasswordEmail(_logger, _viewRender, user, $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}");
            return View((role, email, isMailSended));
        }
        [HttpGet]
        public async Task<IActionResult> ResetPassword(string access_token)
        {
            if (string.IsNullOrWhiteSpace(access_token)) return BadRequest();
            var claims = AuthOptions.ValidateToken(access_token);
            var email = claims?.Identity?.Name;
            if (string.IsNullOrEmpty(email)) return BadRequest();
            var roleValue = claims.FindFirst(ClaimsIdentity.DefaultRoleClaimType)?.Value;
            if (string.IsNullOrEmpty(roleValue)) return BadRequest();
            var role = GetRoleFromString(roleValue);
            if (role == Roles.None) return BadRequest();
            var user = await userRepository.FindByEmail(email, role);
            if (user == null) return BadRequest();
            ViewBag.AccessToken = access_token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(string access_token, string password)
        {
            if (string.IsNullOrWhiteSpace(access_token)||string.IsNullOrWhiteSpace(password)) return BadRequest();
            var claims = AuthOptions.ValidateToken(access_token);
            var email = claims?.Identity?.Name;
            if (string.IsNullOrEmpty(email)) return BadRequest();
            var roleValue = claims.FindFirst(ClaimsIdentity.DefaultRoleClaimType)?.Value;
            if (string.IsNullOrEmpty(roleValue)) return BadRequest();
            var role = GetRoleFromString(roleValue);
            if (role == Roles.None) return BadRequest();
            var user = await userRepository.FindByEmail(email, role);
            if (user == null) return BadRequest();
            var (refresh_token, expire) = AuthOptions.GenerateRefreshToken();
            user.RefreshToken = refresh_token;
            user.RefreshTokenExpire = expire;
            user.PasswordHash = UserRepository.GetPasswordHash(password);
            user = await userRepository.Update(user);
            (string loginController, string loginAction) linkData = user.Role switch
            {
                Roles.None => throw new NotImplementedException(),
                Roles.Admin => ("Pharmacy", "Login"),
                Roles.ApiUser => ("Home", "ApiLogin"),
                Roles.Customer => throw new NotImplementedException(),
                Roles.PharmacyUser => ("Pharmacy", "Login"),
                _ => throw new NotImplementedException(),
            };
            return View(linkData);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
