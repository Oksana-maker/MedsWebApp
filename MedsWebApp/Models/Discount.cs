using MedsWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedsWebApp.Models
{
    public class Discount : BaseModel, IViewModel<DiscountViewModel>
    {
        [Required]
        public string Name { get; set; }
        public List<MedicineInPharmacy> Medicines { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime DiscountStart { get; set; } = DateTime.Now;
        [Required]
        public DateTime DiscountEnd { get; set; } = DateTime.Now;
        [Required]
        public int Value { get; set; }

        public DiscountViewModel AsViewModel() => new DiscountViewModel
        {
            Id = Id,
            Description = Description,
            DiscountEnd = DiscountEnd,
            DiscountStart = DiscountStart,
            Name = Name,
            Value = Value
        };
    }
}
