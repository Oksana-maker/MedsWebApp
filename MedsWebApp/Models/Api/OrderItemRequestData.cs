using Newtonsoft.Json;

namespace MedsWebApp.Models.Api
{
    public class OrderItemRequestData
    {
        [JsonProperty("medicine_in_pharmacy_id")]
        public int MedicineInPharmacyId { get; set; }
        public int Count { get; set; }
    }
}
