using MedsWebApp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedsWebApp.Models
{
    public class MedicineInPharmacy : BaseModel, IViewModel<MedicineInPharmacyViewModel>
    {
        [Required]
        public int PharmacyId { get; set; }
        public Pharmacy Pharmacy { get; set; }
        [Required]
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
        [Required]
        public int AvailableCount { get; set; }
        [Required]
        public double Price { get; set; }
        public int? DiscountId { get; set; }
        public Discount Discount { get; set; }

        public MedicineInPharmacyViewModel AsViewModel()
        {
            var vm = new MedicineInPharmacyViewModel
            {
                Id = Id,
                AvailableCount = AvailableCount,
                DiscountId = DiscountId,
                MedicineId = MedicineId,
                MedicineName = Medicine?.Name,
                PharmacyAddress = Pharmacy?.Address,
                PharmacyId = PharmacyId,
                PharmacyName = Pharmacy?.Name,
                Price = Price
            };
            if (Discount == null || Discount.DiscountEnd < DateTime.Now || Discount.DiscountStart > DateTime.Now) vm.DiscountValue = null;
            else vm.DiscountValue = Discount.Value;
            return vm;
        }

        

    }
}
