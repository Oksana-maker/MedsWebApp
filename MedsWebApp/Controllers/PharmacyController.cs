using MedsWebApp.Models;
using MedsWebApp.Repositories;
using MedsWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NETCore.MailKit.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MedsWebApp.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = Models.User.PharmacyUserRole + "," + Models.User.AdminRole)]
    [Route("[controller]")]
    public class PharmacyController : Controller
    {
        private readonly UserRepository userRepository;
        private readonly PharmacyRepository pharmacyRepository;
        private readonly ILogger<PharmacyController> _logger;
        private readonly ApplicationContext _dbContext;
        private readonly IEmailService mailService;
        private readonly ViewRender _viewRender;


        private readonly MedicineRepository medicineRepository;
        private readonly CategoryRepository categoryRepository;
        private readonly ManufacturerRepository manufacturerRepository;
        private readonly DiscountRepository discountRepository;
        private readonly MedicineInPharmacyRepository medicineInPharmacyRepository;
        private const int PAGE_SIZE = 7;

        public PharmacyController(ILogger<PharmacyController> logger, ApplicationContext context, IEmailService emailService, ViewRender viewRender)
        {
            _logger = logger;
            _dbContext = context;
            userRepository = new UserRepository(context);
            pharmacyRepository = new PharmacyRepository(context);
            mailService = emailService;
            _viewRender = viewRender;


            medicineRepository = new MedicineRepository(context);
            categoryRepository = new CategoryRepository(context);
            manufacturerRepository = new ManufacturerRepository(context);
            discountRepository = new DiscountRepository(context);
            medicineInPharmacyRepository = new MedicineInPharmacyRepository(context);
        }
        [AllowAnonymous]
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.HasClaim(System.Security.Claims.ClaimsIdentity.DefaultRoleClaimType, Models.User.AdminRole)) return RedirectToAction("Medicines");
                return RedirectToAction("Orders");
            }
            return RedirectToAction("Login");
        }
        [AllowAnonymous]
        [Route("[action]")]
        [HttpGet]
        public IActionResult Login()
        {
            ViewData["TargetController"] = "Pharmacy";
            ViewData["TargetAction"] = "Login";
            ViewData["TargetRegistrationAction"] = "Registration";
            return View("Login");
        }
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || !new EmailAddressAttribute().IsValid(email)) return BadRequest();
            var user = await userRepository.Login(email, password, Models.User.Roles.PharmacyUser);
            ViewData["TargetController"] = "Pharmacy";
            ViewData["TargetAction"] = "Login";
            ViewData["TargetRegistrationAction"] = "Registration";
            if (user != null)
            {
                if (!user.IsEmailConfirmed) return View((email, true, false, user.Role));
                var refreshTokenData = AuthOptions.GenerateRefreshToken();
                user.RefreshToken = refreshTokenData.refreshToken;
                user.RefreshTokenExpire = refreshTokenData.expire;
                user = await userRepository.Update(user);
                var (accessToken, expire) = AuthOptions.GenerateJWT(user);
                Response.Cookies.Append("access_token", accessToken, new CookieOptions { HttpOnly = true, Expires = expire.AddHours(1) });
                Response.Cookies.Append("refresh_token", user.RefreshToken.ToString(), new CookieOptions { HttpOnly = true, Expires = refreshTokenData.expire });
                if (user.Role == Models.User.Roles.Admin) return RedirectToAction("Medicines");
                return RedirectToAction("Orders");
            }
            return View((email, false, false, Models.User.Roles.None));
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Models.User.PharmacyUserRole)]
        [HttpGet("[action]")]
        public async Task<IActionResult> Orders()
        {
            var userEmail = User?.Identity?.Name;
            if (string.IsNullOrEmpty(userEmail)) return Forbid();
            var user = await userRepository.FindByEmail(userEmail, Models.User.Roles.PharmacyUser);
            if (user == null|| user.PharmacyId==null) return Forbid();
            var pharmacy = await pharmacyRepository.FindById(user.PharmacyId.Value);
            if (pharmacy == null) return Forbid();
            return View(pharmacy);
        }
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> RefreshToken(Guid refresh_token, string redirect)
        {
            if (refresh_token == Guid.Empty) return BadRequest();
            var user = await userRepository.FindByRefreshToken(refresh_token, Models.User.Roles.PharmacyUser);
            if (user == null)
            {
                user = await userRepository.FindByRefreshToken(refresh_token, Models.User.Roles.Admin);
                if(user == null) return RedirectToAction("Login");
            }
            var (newRefreshToken, expire) = AuthOptions.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpire = expire;
            user = await userRepository.Update(user);
            var (accessToken, expireAT) = AuthOptions.GenerateJWT(user);
            Response.Cookies.Append("access_token", accessToken, new CookieOptions { HttpOnly = true, Expires = expireAT.AddHours(1) });
            Response.Cookies.Append("refresh_token", user.RefreshToken.ToString(), new CookieOptions { HttpOnly = true, Expires = expire });
            return Redirect(redirect);
        }

        [HttpGet("[action]")]
        public IActionResult LogOut()
        {
            Response.Cookies.Delete("access_token");
            Response.Cookies.Delete("refresh_token");
            return RedirectToAction("Login");
        }
        [AllowAnonymous]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> Registration()
        {
            ViewData["TargetController"] = "Pharmacy";
            ViewData["TargetAction"] = "Registration";
            ViewData["TargetLoginAction"] = "Login";
            var pharmacies = await pharmacyRepository.GetAll();
            ViewBag.Pharmacies = pharmacies;
            return View("Registration");
        }
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Registration(string email, string password, int pharmacyId, string name)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || !new EmailAddressAttribute().IsValid(email)) return BadRequest();
            var user = await userRepository.FindByEmail(email, Models.User.Roles.PharmacyUser);
            if(user == null) user = await userRepository.FindByEmail(email, Models.User.Roles.Admin);
            var pharmacy = await pharmacyRepository.FindById(pharmacyId);
            ViewData["TargetController"] = "Pharmacy";
            ViewData["TargetAction"] = "Registration";
            ViewData["TargetLoginAction"] = "Login";
            if (user != null || pharmacy == null) return View("Registration", (email, false, false));
            user = await userRepository.Register(email, password, Models.User.Roles.PharmacyUser, name, pharmacy.Id);
            var isMailSended = await mailService.SendRegistrationPharmacyUserEmail(_logger, _viewRender, user, pharmacy, $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}");
            if (!isMailSended) return View("Registration", (email, true, false));
            return View("Registration", (email, true, true));
        }

        #region AdminPanel
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Models.User.AdminRole)]
        [HttpGet("[action]/{page:int:min(1)?}")]
        public async Task<IActionResult> Medicines(int page)
        {
            var (medicines, totalPage) = await medicineRepository.GetAll(page, PAGE_SIZE);
            ViewBag.Page = page < 1 ? 1 : page;
            ViewBag.ItemPartial = "_MedicineItem";
            ViewBag.EditRoute = "medsedit";
            ViewBag.ShowRoute = "medsshow";
            ViewBag.AddRoute = "medsadd";
            ViewBag.Title = "Медикаменти";
            return View("Items", (medicines.Cast<BaseModel>(), totalPage));
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Models.User.AdminRole)]
        [HttpGet("[action]/{page:int:min(1)?}")]
        public async Task<IActionResult> Manufacturers(int page)
        {
            var (manufacturers, totalPage) = await manufacturerRepository.GetAll(page, PAGE_SIZE);
            ViewBag.Page = page < 1 ? 1 : page;
            ViewBag.ItemPartial = "_ManufacturerItem";
            ViewBag.EditRoute = "manedit";
            ViewBag.ShowRoute = "manshow";
            ViewBag.AddRoute = "manadd";
            ViewBag.Title = "Виробники";
            return View("Items", (manufacturers.Cast<BaseModel>(), totalPage));
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Models.User.AdminRole)]
        [HttpGet("[action]/{page:int:min(1)?}")]
        public async Task<IActionResult> Categories(int page)
        {
            var (categories, totalPage) = await categoryRepository.GetAll(page, PAGE_SIZE);
            ViewBag.Page = page < 1 ? 1 : page;
            ViewBag.ItemPartial = "_CategoryItem";
            ViewBag.EditRoute = "catedit";
            ViewBag.ShowRoute = "catshow";
            ViewBag.AddRoute = "catadd";
            ViewBag.Title = "Категорії";
            return View("Items", (categories.Cast<BaseModel>(), totalPage));
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Models.User.AdminRole)]
        [HttpGet("[action]/{page:int:min(1)?}")]
        public async Task<IActionResult> Pharmacies(int page)
        {
            var (categories, totalPage) = await pharmacyRepository.GetAll(page, PAGE_SIZE);
            ViewBag.Page = page < 1 ? 1 : page;
            ViewBag.ItemPartial = "_PharmacyItem";
            ViewBag.EditRoute = "pharedit";
            ViewBag.ShowRoute = "pharshow";
            ViewBag.AddRoute = "pharadd";
            ViewBag.Title = "Аптеки";
            return View("Items", (categories.Cast<BaseModel>(), totalPage));
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Models.User.AdminRole)]
        [HttpGet("[action]/{page:int:min(1)?}")]
        public async Task<IActionResult> Discounts(int page)
        {
            var (discounts, totalPage) = await discountRepository.GetAll(page, PAGE_SIZE);
            ViewBag.Page = page < 1 ? 1 : page;
            ViewBag.ItemPartial = "_DiscountItem";
            ViewBag.EditRoute = "discedit";
            ViewBag.ShowRoute = "discshow";
            ViewBag.AddRoute = "discadd";
            ViewBag.Title = "Знижки";
            return View("Items", (discounts.Cast<BaseModel>(), totalPage));
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Models.User.AdminRole)]
        [HttpGet("[action]/{page:int:min(1)?}")]
        public async Task<IActionResult> MedicinesInPharmacy(int page)
        {
            var (mips, totalPage) = await medicineInPharmacyRepository.GetAll(page, PAGE_SIZE);
            ViewBag.Page = page < 1 ? 1 : page;
            ViewBag.ItemPartial = "_MedicineInPharmacyItem";
            ViewBag.EditRoute = "mipedit";
            ViewBag.ShowRoute = "mipshow";
            ViewBag.AddRoute = "mipadd";
            ViewBag.Title = "Медикаменти в аптеках";
            return View("Items", (mips.Cast<BaseModel>(), totalPage));
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Models.User.AdminRole)]
        [HttpGet("Medicines/[action]", Name = "medsadd")]
        [HttpGet("Manufacturers/[action]", Name = "manadd")]
        [HttpGet("Categories/[action]", Name = "catadd")]
        [HttpGet("Pharmacies/[action]", Name = "pharadd")]
        [HttpGet("Discounts/[action]", Name = "discadd")]
        [HttpGet("MedicinesInPharmacy/[action]", Name = "mipadd")]
        public async Task<IActionResult> Add(bool? success)
        {
            ViewBag.IsSuccess = success;
            var path = HttpContext.Request.Path;
            if (path.StartsWithSegments("/Pharmacy/Medicines"))
            {
                ViewBag.Categories = await categoryRepository.GetAll();
                ViewBag.Manufacturers = await manufacturerRepository.GetAll();
                ViewBag.PostRoute = "medsaddpost";
                return View("MedicineItemEdit", new Medicine());
            }
            else if (path.StartsWithSegments("/Pharmacy/Manufacturers"))
            {
                ViewBag.PostRoute = "manaddpost";
                return View("ManufacturerItemEdit", new Manufacturer());
            }
            else if (path.StartsWithSegments("/Pharmacy/Categories"))
            {
                ViewBag.Categories = await categoryRepository.GetAll();
                ViewBag.PostRoute = "cataddpost";
                return View("CategoryItemEdit", new Category());
            }
            else if (path.StartsWithSegments("/Pharmacy/Pharmacies"))
            {
                ViewBag.PostRoute = "pharaddpost";
                return View("PharmacyItemEdit", new Pharmacy());
            }
            else if (path.StartsWithSegments("/Pharmacy/Discounts"))
            {
                ViewBag.PostRoute = "discaddpost";
                return View("DiscountItemEdit", new Discount());
            }
            else if (path.StartsWithSegments("/Pharmacy/MedicinesInPharmacy"))
            {
                ViewBag.Medicines = await medicineRepository.GetAll();
                ViewBag.Pharmacies = await pharmacyRepository.GetAll();
                ViewBag.Discounts = await discountRepository.GetAll();
                ViewBag.PostRoute = "mipaddpost";
                return View("MedicineInPharmacyItemEdit", new MedicineInPharmacy());
            }
            else return NotFound();
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Models.User.AdminRole)]
        [HttpGet("Medicines/[action]/{id:int:min(1)}", Name = "medsedit")]
        [HttpGet("Manufacturers/[action]/{id:int:min(1)}", Name = "manedit")]
        [HttpGet("Categories/[action]/{id:int:min(1)}", Name = "catedit")]
        [HttpGet("Pharmacies/[action]/{id:int:min(1)}", Name = "pharedit")]
        [HttpGet("Discounts/[action]/{id:int:min(1)}", Name = "discedit")]
        [HttpGet("MedicinesInPharmacy/[action]/{id:int:min(1)}", Name = "mipedit")]
        public async Task<IActionResult> Edit(int id, bool? success)
        {
            ViewBag.IsSuccess = success;
            var path = HttpContext.Request.Path;
            if (path.StartsWithSegments("/Pharmacy/Medicines"))
            {
                var medicine = await medicineRepository.FindById(id);
                if (medicine == null) return NotFound();
                ViewBag.Categories = await categoryRepository.GetAll();
                ViewBag.Manufacturers = await manufacturerRepository.GetAll();
                ViewBag.PostRoute = "medseditpost";
                return View("MedicineItemEdit", medicine);
            }
            else if (path.StartsWithSegments("/Pharmacy/Manufacturers"))
            {
                var manufacturer = await manufacturerRepository.FindById(id);
                if (manufacturer == null) return NotFound();
                ViewBag.PostRoute = "maneditpost";
                return View("ManufacturerItemEdit", manufacturer);
            }
            else if (path.StartsWithSegments("/Pharmacy/Categories"))
            {
                var category = await categoryRepository.FindById(id);
                if (category == null) return NotFound();
                ViewBag.Categories = await categoryRepository.GetAll();
                ViewBag.PostRoute = "cateditpost";
                return View("CategoryItemEdit", category);
            }
            else if (path.StartsWithSegments("/Pharmacy/Pharmacies"))
            {
                var pharmacy = await pharmacyRepository.FindById(id);
                if (pharmacy == null) return NotFound();
                ViewBag.PostRoute = "phareditpost";
                return View("PharmacyItemEdit", pharmacy);
            }
            else if (path.StartsWithSegments("/Pharmacy/Discounts"))
            {
                var discount = await discountRepository.FindById(id);
                if (discount == null) return NotFound();
                ViewBag.PostRoute = "disceditpost";
                return View("DiscountItemEdit", discount);
            }
            else if (path.StartsWithSegments("/Pharmacy/MedicinesInPharmacy"))
            {
                var mip = await medicineInPharmacyRepository.FindByIdEager(id);
                if (mip == null) return NotFound();
                ViewBag.Medicines = await medicineRepository.GetAll();
                ViewBag.Pharmacies = await pharmacyRepository.GetAll();
                ViewBag.Discounts = await discountRepository.GetAll();
                ViewBag.PostRoute = "mipeditpost";
                return View("MedicineInPharmacyItemEdit", mip);
            }
            else return NotFound();
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Models.User.AdminRole)]
        [HttpGet("Medicines/[action]/{id:int:min(1)}", Name = "medsshow")]
        [HttpGet("Manufacturers/[action]/{id:int:min(1)}", Name = "manshow")]
        [HttpGet("Categories/[action]/{id:int:min(1)}", Name = "catshow")]
        [HttpGet("Pharmacies/[action]/{id:int:min(1)}", Name = "pharshow")]
        [HttpGet("Discounts/[action]/{id:int:min(1)}", Name = "discshow")]
        [HttpGet("MedicinesInPharmacy/[action]/{id:int:min(1)}", Name = "mipshow")]
        public async Task<IActionResult> Show(int id)
        {
            var path = HttpContext.Request.Path;
            BaseModel model;
            if (path.StartsWithSegments("/Pharmacy/Medicines"))
            {
                model = await medicineRepository.FindById(id);
                ViewBag.DeleteRoute = "medsdelete";
            }
            else if (path.StartsWithSegments("/Pharmacy/Manufacturers"))
            {
                model = await manufacturerRepository.FindById(id);
                ViewBag.DeleteRoute = "mandelete";
            }
            else if (path.StartsWithSegments("/Pharmacy/Categories"))
            {
                model = await categoryRepository.FindById(id);
                ViewBag.DeleteRoute = "catdelete";
            }
            else if (path.StartsWithSegments("/Pharmacy/Pharmacies"))
            {
                model = await pharmacyRepository.FindById(id);
                ViewBag.DeleteRoute = "phardelete";
            }
            else if (path.StartsWithSegments("/Pharmacy/Discounts"))
            {
                model = await discountRepository.FindById(id);
                ViewBag.DeleteRoute = "discdelete";
            }
            else if (path.StartsWithSegments("/Pharmacy/MedicinesInPharmacy"))
            {
                model = await medicineInPharmacyRepository.FindByIdEager(id);
                ViewBag.DeleteRoute = "mipdelete";
            }
            else return NotFound();
            if (model == null) return NotFound();
            return View(model);
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Models.User.AdminRole)]
        [HttpGet("Medicines/[action]/{id:int:min(1)}", Name = "medsdelete")]
        [HttpGet("Manufacturers/[action]/{id:int:min(1)}", Name = "mandelete")]
        [HttpGet("Categories/[action]/{id:int:min(1)}", Name = "catdelete")]
        [HttpGet("Pharmacies/[action]/{id:int:min(1)}", Name = "phardelete")]
        [HttpGet("Discounts/[action]/{id:int:min(1)}", Name = "discdelete")]
        [HttpGet("MedicinesInPharmacy/[action]/{id:int:min(1)}", Name = "mipdelete")]
        public async Task<IActionResult> Delete(int id)
        {
            var path = HttpContext.Request.Path;
            var redirectPath = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            BaseModel model;
            string modelName;
            if (path.StartsWithSegments("/Pharmacy/Medicines"))
            {
                model = await medicineRepository.Delete(id);
                modelName = (model as Medicine)?.Name;
                redirectPath += "/Pharmacy/Medicines";
            }
            else if (path.StartsWithSegments("/Pharmacy/Manufacturers"))
            {
                model = await manufacturerRepository.Delete(id);
                modelName = (model as Manufacturer)?.Name;
                redirectPath += "/Pharmacy/Manufacturers";
            }
            else if (path.StartsWithSegments("/Pharmacy/Categories"))
            {
                model = await categoryRepository.Delete(id);
                modelName = (model as Category)?.Name;
                redirectPath += "/Pharmacy/Categories";
            }
            else if (path.StartsWithSegments("/Pharmacy/Pharmacies"))
            {
                model = await pharmacyRepository.Delete(id);
                modelName = (model as Pharmacy)?.Name;
                redirectPath += "/Pharmacy/Pharmacies";
            }
            else if (path.StartsWithSegments("/Pharmacy/Discounts"))
            {
                model = await discountRepository.Delete(id);
                modelName = (model as Discount)?.Name;
                redirectPath += "/Pharmacy/Discounts";
            }
            else if (path.StartsWithSegments("/Pharmacy/MedicinesInPharmacy"))
            {
                model = await medicineInPharmacyRepository.Delete(id);
                modelName = (model as MedicineInPharmacy)?.Medicine?.Name;
                redirectPath += "/Pharmacy/MedicinesInPharmacy";
            }
            else return NotFound();
            if (model == null) return NotFound();
            return View((modelName, redirectPath));
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Models.User.AdminRole)]
        [HttpPost("Medicines/[action]/{id:int:min(1)}", Name = "medseditpost")]
        public async Task<IActionResult> Edit(Medicine model)
        {
            if (model.ImageURLData != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(model.ImageURLData.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)model.ImageURLData.Length);
                }
                var extension = Path.GetExtension(model.ImageURLData.FileName);
                var base64String = Convert.ToBase64String(imageData);
                model.ImageURL = $"data:image/{extension};base64,{base64String}";
            }
            else
            {
                var oldMed = await medicineRepository.FindById(model.Id);
                model.ImageURL = oldMed?.ImageURL;
            }
            var medicine = await medicineRepository.Update(model);
            if (medicine == null) return NotFound();
            return RedirectToRoute("medsedit", new { id = medicine.Id, success = true });
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Models.User.AdminRole)]
        [HttpPost("Manufacturers/[action]/{id:int:min(1)}", Name = "maneditpost")]
        public async Task<IActionResult> Edit(Manufacturer model)
        {
            var manufacturer = await manufacturerRepository.Update(model);
            if (manufacturer == null) return NotFound();
            return RedirectToRoute("manedit", new { id = manufacturer.Id, success = true });
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Models.User.AdminRole)]
        [HttpPost("Categories/[action]/{id:int:min(1)}", Name = "cateditpost")]
        public async Task<IActionResult> Edit(Category model)
        {
            var category = await categoryRepository.Update(model);
            if (category == null) return NotFound();
            return RedirectToRoute("catedit", new { id = category.Id, success = true });
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Models.User.AdminRole)]
        [HttpPost("Pharmacies/[action]/{id:int:min(1)}", Name = "phareditpost")]
        public async Task<IActionResult> Edit(Pharmacy model)
        {
            var pharmacy = await pharmacyRepository.Update(model);
            if (pharmacy == null) return NotFound();
            return RedirectToRoute("pharedit", new { id = pharmacy.Id, success = true });
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Models.User.AdminRole)]
        [HttpPost("Discounts/[action]/{id:int:min(1)}", Name = "disceditpost")]
        public async Task<IActionResult> Edit(Discount model)
        {
            var discount = await discountRepository.Update(model);
            if (discount == null) return NotFound();
            return RedirectToRoute("discedit", new { id = discount.Id, success = true });
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Models.User.AdminRole)]
        [HttpPost("MedicinesInPharmacy/[action]/{id:int:min(1)}", Name = "mipeditpost")]
        public async Task<IActionResult> Edit(MedicineInPharmacy model)
        {
            var mip = await medicineInPharmacyRepository.Update(model);
            if (mip == null) return NotFound();
            return RedirectToRoute("mipedit", new { id = mip.Id, success = true });
        }



        [Authorize(AuthenticationSchemes = "Bearer", Roles = Models.User.AdminRole)]
        [HttpPost("Medicines/[action]", Name = "medsaddpost")]
        public async Task<IActionResult> Add(Medicine model)
        {
            if (model.ImageURLData != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(model.ImageURLData.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)model.ImageURLData.Length);
                }
                var extension = Path.GetExtension(model.ImageURLData.FileName);
                var base64String = Convert.ToBase64String(imageData);
                model.ImageURL = $"data:image/{extension};base64,{base64String}";
            }
            var medicine = await medicineRepository.Insert(model);
            if (medicine == null) return NotFound();
            return RedirectToRoute("medsedit", new { id = medicine.Id, success = true });
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Models.User.AdminRole)]
        [HttpPost("Manufacturers/[action]", Name = "manaddpost")]
        public async Task<IActionResult> Add(Manufacturer model)
        {
            var manufacturer = await manufacturerRepository.Insert(model);
            if (manufacturer == null) return NotFound();
            return RedirectToRoute("manedit", new { id = manufacturer.Id, success = true });
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Models.User.AdminRole)]
        [HttpPost("Categories/[action]", Name = "cataddpost")]
        public async Task<IActionResult> Add(Category model)
        {
            var category = await categoryRepository.Insert(model);
            if (category == null) return NotFound();
            return RedirectToRoute("catedit", new { id = category.Id, success = true });
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Models.User.AdminRole)]
        [HttpPost("Pharmacies/[action]", Name = "pharaddpost")]
        public async Task<IActionResult> Add(Pharmacy model)
        {
            var pharmacy = await pharmacyRepository.Insert(model);
            if (pharmacy == null) return NotFound();
            return RedirectToRoute("pharedit", new { id = pharmacy.Id, success = true });
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Models.User.AdminRole)]
        [HttpPost("Discounts/[action]", Name = "discaddpost")]
        public async Task<IActionResult> Add(Discount model)
        {
            var discount = await discountRepository.Insert(model);
            if (discount == null) return NotFound();
            return RedirectToRoute("discedit", new { id = discount.Id, success = true });
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Models.User.AdminRole)]
        [HttpPost("MedicinesInPharmacy/[action]", Name = "mipaddpost")]
        public async Task<IActionResult> Add(MedicineInPharmacy model)
        {
            var mip = await medicineInPharmacyRepository.Insert(model);
            if (mip == null) return NotFound();
            return RedirectToRoute("mipedit", new { id = mip.Id, success = true });
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Models.User.AdminRole)]
        [HttpGet("[action]")]
        public IActionResult CreateAdmin()
        {
            return View();
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = Models.User.AdminRole)]
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateAdmin (string email, string password, string name)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || !new EmailAddressAttribute().IsValid(email)) return BadRequest();
            var user = await userRepository.FindByEmail(email, Models.User.Roles.Admin);
            if(user == null) user = await userRepository.FindByEmail(email, Models.User.Roles.PharmacyUser);
            if (user != null) return View("CreateAdmin", (email, false, false));
            user = await userRepository.Register(email, password, Models.User.Roles.Admin, name, null);
            var isMailSended = await mailService.SendRegistrationAdminUserEmail(_logger, _viewRender, user, $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}");
            if (!isMailSended) return View("CreateAdmin", (email, true, false));
            return View("CreateAdmin", (email, true, true));
        }
        #endregion
    }
}
