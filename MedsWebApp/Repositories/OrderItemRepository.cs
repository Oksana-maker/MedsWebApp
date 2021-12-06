using MedsWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedsWebApp.Repositories
{
    public class OrderItemRepository : BaseRepository<OrderItem>
    {
        public OrderItemRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }

        public Task<OrderItem[]> LoadNewForPharmacy(int pharmacyId) =>
            Get(oi => oi.MedicineInPharmacy.PharmacyId == pharmacyId && (oi.Order.Status == Order.OrderStatus.New || oi.Order.Status == Order.OrderStatus.Ready),
                true)
            .Include(oi => oi.Order)
            .Include(oi => oi.MedicineInPharmacy)
                .ThenInclude(mip => mip.Medicine)
            .Include(oi => oi.MedicineInPharmacy)
                .ThenInclude(mip => mip.Pharmacy)
            .ToArrayAsync();
        public Task<OrderItem[]> LoadById(int orderId, int pharmacyId) =>
            Get(oi => oi.OrderId == orderId && oi.MedicineInPharmacy.PharmacyId == pharmacyId,
                true)
            .Include(oi => oi.Order)
            .Include(oi => oi.MedicineInPharmacy)
                .ThenInclude(mip => mip.Medicine)
            .Include(oi => oi.MedicineInPharmacy)
                .ThenInclude(mip => mip.Pharmacy)
            .ToArrayAsync();

    }
}
