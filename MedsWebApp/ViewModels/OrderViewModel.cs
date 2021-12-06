using MedsWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedsWebApp.ViewModels
{
    public class OrderViewModel : BaseViewModel
    {
        public Order.OrderStatus Status { get; internal set; }
        public double Price { get; internal set; }
        public OrderItemViewModel[] OrderItems { get; internal set; }
        public DateTime Create { get; internal set; }
        public int UserId { get; internal set; }
    }
}
