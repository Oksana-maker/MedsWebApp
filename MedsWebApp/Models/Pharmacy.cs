using MedsWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedsWebApp.Models
{
    public class Pharmacy : BaseModel, IViewModel<PharmacyViewModel>
    {
        [Required]
        public string Name { get; set; }
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string WorkTime { get; set; }
        public List<MedicineInPharmacy> Medicines { get; set; }
        public List<User> Users { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public PharmacyViewModel AsViewModel() => new PharmacyViewModel
        {
            Id = Id,
            Name = Name,
            Phone = Phone,
            Address = Address,
            WorkTime = WorkTime,
         
        };
    }
}
