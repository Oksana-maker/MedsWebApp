using MedsWebApp.Models;
using MedsWebApp.Repositories;
using MedsWebApp.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using NETCore.MailKit.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedsWebApp.Services
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Models.User.PharmacyUserRole)]
    public class OrdersInfoHub : Hub
    {
        private readonly PharmacyRepository pharmacyRepository;
        private readonly OrderItemRepository orderItemsRepository;
        private readonly OrdersRepository ordersRepository;
        private readonly UserRepository userRepository;
        private readonly MedicineInPharmacyRepository medicineInPharmacyRepository;
        private readonly IEmailService mailService;
        private readonly ViewRender _viewRender;
        private readonly ILogger<OrdersInfoHub> _logger;
        public class OrderStatusMessage
        {
            public int OrderId { get; set; }
            public int NewStatus { get; set; }
        }

        public OrdersInfoHub(ApplicationContext applicationContext, IEmailService emailService, ViewRender viewRender, ILogger<OrdersInfoHub> logger)
        {
            pharmacyRepository = new PharmacyRepository(applicationContext);
            orderItemsRepository = new OrderItemRepository(applicationContext);
            ordersRepository = new OrdersRepository(applicationContext);
            userRepository = new UserRepository(applicationContext);
            medicineInPharmacyRepository = new MedicineInPharmacyRepository(applicationContext);
            mailService = emailService;
            _viewRender = viewRender;
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            var user = await userRepository.FindByEmail(Context.User.Identity.Name, User.Roles.PharmacyUser);
            if (user == null|| user.PharmacyId==null) return;
            var pharmacy = await pharmacyRepository.FindById(user.PharmacyId.Value);
            if (pharmacy == null) return;
            await Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
            var orderItems = await orderItemsRepository.LoadNewForPharmacy(pharmacy.Id);
            var orders = orderItems.Select(oi=>oi.Order).Distinct().AsViewModel().ToArray();
            
            await Clients.Caller.SendAsync("FirstLoad", JsonConvert.SerializeObject(orders));
            await base.OnConnectedAsync();
        }
        
        public async Task SetOrderStatus(OrderStatusMessage message)
        {
            var newStatus = (Order.OrderStatus)message.NewStatus;
            var user = await userRepository.FindByEmail(Context.User.Identity.Name, User.Roles.PharmacyUser);
            if (user == null || user.PharmacyId == null) return;
            var pharmacy = await pharmacyRepository.FindById(user.PharmacyId.Value);
            if (pharmacy == null) return;
            var order = await ordersRepository.FindById(message.OrderId);
            var oldStatus = order.Status;
            if (order == null) return;
            order.Status = newStatus;
            order = await ordersRepository.Update(order);
            var orderInfo = await ordersRepository.LoadEagerNoTracking(order.Id);
            var orderVM = orderInfo.AsViewModel();
            await Clients.Group(Context.User.Identity.Name).SendAsync("UpdateOrder", JsonConvert.SerializeObject(orderVM));
           
            try
            {
                if (oldStatus == Order.OrderStatus.New && newStatus == Order.OrderStatus.Ready)
                    await mailService.SendAsync(orderInfo.User.Email, "Інформація про замовлення в Аптека", _viewRender.Render("_OrderReadyEmailTemplate", orderVM), true);
                else if (oldStatus == Order.OrderStatus.New && newStatus == Order.OrderStatus.Canceled)
                {
                    foreach (var med in orderInfo.OrderItems)
                    {
                        var medInDB = await medicineInPharmacyRepository.FindByIdEager(med.MedicineInPharmacyId);
                        medInDB.AvailableCount += (int)med.Count;
                        await medicineInPharmacyRepository.Update(medInDB);
                    }
                    await mailService.SendAsync(orderInfo.User.Email, "Інформація про замовлення в Аптека", _viewRender.Render("_OrderCanceledEmailTemplate", orderVM), true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Send mail error");
            }
        }

        
    }
}
