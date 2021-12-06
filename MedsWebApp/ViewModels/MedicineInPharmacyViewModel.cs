using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedsWebApp.ViewModels
{
    public class MedicineInPharmacyViewModel : BaseViewModel
    {
        public int PharmacyId { get; set; }
        public string PharmacyName { get; set; }
        public string PharmacyAddress { get; set; }
        public int? DiscountId { get; set; }
        public int? DiscountValue { get; set; }
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public double Price { get; set; }
        public int AvailableCount { get; set; }
        public double? DiscountPrice
        {
            get
            {
                if (!DiscountValue.HasValue) return Price;
                return Price - Price * (((double)DiscountValue.Value) / 100);
            }
        }
        public string ToBase64()
        {
            var json = JsonConvert.SerializeObject(
                new
                {
                    id = Id,
                    medid = MedicineId,
                    medname = MedicineName,
                    pharaddr = PharmacyAddress,
                    avail = AvailableCount,
                    pr = Price
                });
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
        }
    }
}
