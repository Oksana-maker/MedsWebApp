using MedsWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedsWebApp.Repositories
{
    public class OrdersRepository : BaseRepository<Order>
    {
        public OrdersRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }
        public Task<Order> FindById(int orderId) =>
            Get(o => o.Id == orderId).FirstOrDefaultAsync();
        public Task<Order> LoadEagerNoTracking(int orderId) =>
          Get(o => o.Id == orderId, true)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MedicineInPharmacy)
                        .ThenInclude(m => m.Medicine)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MedicineInPharmacy)
                        .ThenInclude(m => m.Pharmacy)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MedicineInPharmacy)
                        .ThenInclude(m => m.Discount)
                .Include(m => m.User)
                .FirstOrDefaultAsync();
    }
}
