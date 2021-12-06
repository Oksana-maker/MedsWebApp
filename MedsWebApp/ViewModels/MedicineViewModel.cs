using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedsWebApp.ViewModels
{
    public class MedicineViewModel : BaseViewModel
    {
        public string ImageURL { get; set; }

        public const string NotAvailable = "Немає в наявності";
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public string ManufacturerName { get; set; }
        public int ManufacturerId { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public string Description { get; set; }
        public string DisplayPrice => MinPrice > 0 && MaxPrice > 0 ? 
            MaxPrice == MinPrice ? 
                $"{MaxPrice:F2} грн." : 
                $"{MinPrice:F2} - {MaxPrice:F2} грн." :
            NotAvailable; 
    }
}
