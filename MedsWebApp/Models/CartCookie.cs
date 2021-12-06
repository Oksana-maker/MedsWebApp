using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedsWebApp.Models
{
    public class CartCookie
    {
        [JsonProperty("mid")]
        public int MedicineInPharmacyId { get; set; }
        [JsonProperty("c")]
        public int Count { get; set; }
    }
}
