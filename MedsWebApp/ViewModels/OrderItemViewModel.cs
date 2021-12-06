using MedsWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedsWebApp.ViewModels
{
    public class OrderItemViewModel : BaseViewModel
    {
        public double Price { get; internal set; }
        public uint Count { get; internal set; }
        public string MedicineName { get; internal set; }
        public int MedicineInPharmacyId { get; internal set; }
        public int PharmacyId { get; internal set; }
        public string PharmacyAddress { get; internal set; }
        public string PharmacyName { get; internal set; }

        public string PharmacyTel { get; internal set; }

    }
}
