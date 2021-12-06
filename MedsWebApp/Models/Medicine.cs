using MedsWebApp.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MedsWebApp.Models
{
    public class Medicine : BaseModel, IViewModel<MedicineViewModel>
    {
        [NotMapped]
        public IFormFile ImageURLData { get; set; }

        public string ImageURL { get; set; } 
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [Required]
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public List<MedicineInPharmacy> MedicinesInPharmacy { get; set; }

        public MedicineViewModel AsViewModel()
        {
            var vm = new MedicineViewModel
            {
                Id = Id,
                Name = Name,
                CategoryId = CategoryId,
                CategoryName = Category?.Name,
                ManufacturerId = ManufacturerId,
                ManufacturerName = Manufacturer?.Name,
                Description = Description,
                ImageURL = ImageURL
            };
            if (MedicinesInPharmacy == null || MedicinesInPharmacy.Count <= 0 || !MedicinesInPharmacy.Any(m => m.AvailableCount > 0))
                return vm;

            var availableItems = MedicinesInPharmacy.Where(m => m.AvailableCount > 0).ToArray();
            if (availableItems.Length == 0) return vm;

            double maxPrice = double.MinValue;
            double minPrice = double.MaxValue;
            foreach (var medicineInPharmacy in availableItems)
            {
                if (medicineInPharmacy.Discount is null || medicineInPharmacy.Discount.DiscountEnd < DateTime.Now || medicineInPharmacy.Discount.DiscountStart > DateTime.Now)
                {
                    if (maxPrice < medicineInPharmacy.Price) maxPrice = medicineInPharmacy.Price;
                    if (minPrice > medicineInPharmacy.Price) minPrice = medicineInPharmacy.Price;
                }
                else
                {
                    var discountPrice = medicineInPharmacy.Price - (medicineInPharmacy.Price * ((double)medicineInPharmacy.Discount.Value / 100));
                    if (maxPrice < discountPrice) maxPrice = discountPrice;
                    if (minPrice > discountPrice) minPrice = discountPrice;
                }
            }
            vm.MaxPrice = maxPrice;
            vm.MinPrice = minPrice;

            return vm;
        }
    }
}
