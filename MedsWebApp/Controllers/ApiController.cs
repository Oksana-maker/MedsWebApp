using MedsWebApp.Models;
using MedsWebApp.Models.Api;
using MedsWebApp.Repositories;
using MedsWebApp.Services;
using MedsWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using NETCore.MailKit.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static MedsWebApp.Models.Order;

namespace MedsWebApp.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = Models.User.ApiUserRole)]
    [ApiController]
    [Route("api")]
    [Route("api/[action]")]
    public class ApiController : ControllerBase
    {
        private readonly MedicineInPharmacyRepository medicineInPharmacyRepository;
        private readonly MedicineRepository medicineRepository;
        private readonly CategoryRepository categoryRepository;
        private readonly UserRepository userRepository;
        private readonly OrdersRepository orderRepository;
        private readonly IEmailService mailService;
        private readonly ViewRender _viewRender;
        private readonly ILogger<ApiController> _logger;
        private readonly PharmacyRepository pharmacyRepository;
        private readonly IHubContext<OrdersInfoHub> _orderInfoHub;
        private const int PAGE_SIZE = 5;


        public ApiController(ApplicationContext context, IEmailService emailService, ViewRender viewRender, ILogger<ApiController> logger, IHubContext<OrdersInfoHub> hubContext)
        {
            medicineInPharmacyRepository = new MedicineInPharmacyRepository(context);
            medicineRepository = new MedicineRepository(context);
            categoryRepository = new CategoryRepository(context);
            userRepository = new UserRepository(context);
            orderRepository = new OrdersRepository(context);
            mailService = emailService;
            _viewRender = viewRender;
            _logger = logger;
            _orderInfoHub = hubContext;
            pharmacyRepository = new PharmacyRepository(context);
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToActionPermanent("ApiLogin", "Home");
        }
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginRequestData data)
        {
            if (data is null || !data.IsValid()) return BadRequest();
            string login = data.Login;
            string password = data.Password;
            var user = await userRepository.Login(login, password, Models.User.Roles.ApiUser);
            if (user == null) return Forbid();
            var (refresh_token, expireRT) = AuthOptions.GenerateRefreshToken();
            user.RefreshToken = refresh_token;
            user.RefreshTokenExpire = expireRT;
            user = await userRepository.Update(user);
            var (accessToken, expireAT) = AuthOptions.GenerateJWT(user);
            return new JsonResult(new
            {
                user_name = login,
                access_token = accessToken,
                access_token_expire = expireAT,
                refresh_token = refresh_token,
                refresh_token_expire = expireRT,
                is_email_comfirmed = user.IsEmailConfirmed
            });
        }
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterRequestData data)
        {
            if (data is null || !data.IsValid()) return BadRequest();
            string login = data.Login;
            string password = data.Password;
            string name = data.Name;
            var user = await userRepository.FindByEmail(login, Models.User.Roles.ApiUser);
            if (user != null) return BadRequest("user is already exists");
            user = await userRepository.Register(login, password, Models.User.Roles.ApiUser, name, null);
            if (user == null) return Forbid();
            var isEmailSended = await mailService.SendRegistrationApiUserEmail(_logger, _viewRender, user, $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}");
            var (accessToken, expireAT) = AuthOptions.GenerateJWT(user);
            return new JsonResult(new
            {
                user_name = login,
                access_token = accessToken,
                access_token_expire = expireAT,
                refresh_token = user.RefreshToken,
                refresh_token_expire = user.RefreshTokenExpire,
                is_email_sended = isEmailSended
            });
        }

        [AllowAnonymous]
        [HttpGet("[action]/{refresh_token:guid}")]
        public async Task<IActionResult> RefreshToken(Guid refresh_token)
        {
            if (refresh_token == Guid.Empty) return BadRequest();
            var user = await userRepository.FindByRefreshToken(refresh_token, Models.User.Roles.ApiUser);
            if (user == null) return Forbid();
            var (newRefreshToken, expire) = AuthOptions.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpire = expire;
            user = await userRepository.Update(user);
            var (accessToken, expireAT) = AuthOptions.GenerateJWT(user);
            return new JsonResult(new
            {
                access_token = accessToken,
                access_token_expire = expireAT,
                refresh_token = newRefreshToken,
                refresh_token_expire = expire,
                user_name = user.Email
            });
        }


        [HttpGet("[action]/{q:minlength(3)}/{page:int:min(1)?}")]
        public async Task<IActionResult> Search(string q, int page = 1)
        {
            var (meds, totalPages) = await medicineRepository.FindByPartOfName(q, page, PAGE_SIZE);
            return new JsonResult(new
            {
                meds = meds.AsViewModel().ToArray(),
                page,
                pageSize = PAGE_SIZE,
                totalPages
            });
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> ResendRegistrationEmail()
        {
            var email = User.Identity.Name;
            if (string.IsNullOrWhiteSpace(email) || !new EmailAddressAttribute().IsValid(email)) return BadRequest();
            var user = await userRepository.FindByEmail(email, Models.User.Roles.ApiUser);
            if (user == null || user.IsEmailConfirmed) return BadRequest("user not found");
            if (await mailService.SendRegistrationApiUserEmail(_logger, _viewRender, user, $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}")) return Ok();
            return BadRequest("Mail is not sended");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ResetPasswordRequest()
        {
            var email = User.Identity.Name;
            if (string.IsNullOrWhiteSpace(email) || !new EmailAddressAttribute().IsValid(email)) return BadRequest();
            var user = await userRepository.FindByEmail(email, Models.User.Roles.ApiUser);
            if (user == null) return NotFound();
            if( await mailService.SendResetPasswordEmail(_logger, _viewRender, user, $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}")) return Ok();
            return Problem("Mail is not sended");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateOrder(CreateOrderRequestData createOrderRequestData)
        {
            if (createOrderRequestData == null || createOrderRequestData.Items == null || createOrderRequestData.Items.Length == 0) return BadRequest();
            var orderItems = new List<OrderItem>();
            foreach (var item in createOrderRequestData.Items)
            {
                var med = await medicineInPharmacyRepository.FindByIdEager(item.MedicineInPharmacyId);
                var count = item.Count;
                if (count <= 0) return BadRequest("Count must be more of 0");
                if (med == null) return BadRequest("Med not found");
                var orderItem = new OrderItem
                {
                    MedicineInPharmacy = med,
                    Price = med.AsViewModel().DiscountPrice ?? med.Price,
                    Count = (uint)count
                };
                orderItems.Add(orderItem);

            }
            var user = await userRepository.FindByEmail(User.Identity.Name, Models.User.Roles.ApiUser);
            if (user == null) return BadRequest("User not found");
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
            return new JsonResult(new
            {
                orders = orders.AsViewModel().ToArray(),
            });
        }
        private async Task NotifyNewOrder(int orderId)
        {
            var order = await orderRepository.LoadEagerNoTracking(orderId);
            var pharmacy = await pharmacyRepository.FindById(order.OrderItems[0].MedicineInPharmacy.PharmacyId);
            foreach (var user in pharmacy.Users)
            {
                await _orderInfoHub.Clients.Group(user.Email).SendAsync("UpdateOrder", JsonConvert.SerializeObject(order.AsViewModel()));
            }
        }

        [HttpGet("[action]/{id:min(0)}")]
        public async Task<IActionResult> GetMedicineInPharmacies(int id)
        {
            if (id <= 0) return BadRequest();
            var medicine = await medicineRepository.FindByIdEager(id);
            if (medicine == null) return NoContent();
            return new JsonResult(new
            {
                medInPharmacies = medicine.MedicinesInPharmacy.AsViewModel().ToArray()
            });
        }

    }
}
