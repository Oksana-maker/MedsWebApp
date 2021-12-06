using MedsWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedsWebApp.Models
{
    public class OrderItem : BaseModel, IViewModel<OrderItemViewModel>
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        [Required]
        public int MedicineInPharmacyId { get; set; }
        public MedicineInPharmacy MedicineInPharmacy { get; set; }
        [Required]
        public uint Count { get; set; }
        [Required]
        public double Price { get; set; }

        public OrderItemViewModel AsViewModel() =>
            new OrderItemViewModel
            {
                Id = Id,
                Count = Count,
                MedicineInPharmacyId = MedicineInPharmacyId,
                MedicineName = MedicineInPharmacy?.Medicine.Name,
                PharmacyId = MedicineInPharmacy.PharmacyId,
                PharmacyName = MedicineInPharmacy?.Pharmacy?.Name,
                PharmacyAddress = MedicineInPharmacy?.Pharmacy?.Address,
                Price = Math.Round(Price * Count, 2),
                PharmacyTel = MedicineInPharmacy?.Pharmacy?.Phone
            };
    }
}
