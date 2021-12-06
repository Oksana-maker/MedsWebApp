using MedsWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedsWebApp.Models
{
    public class Order: BaseModel, IViewModel<OrderViewModel>
    {
        public enum OrderStatus : ushort
        {
            New,
            Ready,
            Canceled,
            Finished
        }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public DateTime CreateDateTime { get; set; }
        [Required]
        public List<OrderItem> OrderItems { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public OrderStatus Status { get; set; }

        public OrderViewModel AsViewModel()
        {
            var vm = new OrderViewModel
            {
                Id = Id,
                UserId = UserId,
                Create = CreateDateTime,
                OrderItems = OrderItems?.AsViewModel().ToArray(),
                Status = Status
            };
            vm.Price = vm.OrderItems is null ? Math.Round(Price, 2) : Math.Round(vm.OrderItems.Sum(oi => oi.Price), 2);
            return vm;
        }

    }
}
