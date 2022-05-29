using Newtonsoft.Json;

namespace Raditap.BusinessLogic.ApiDataObjects.Jobs
{
    public class PrintJobDto
    {
        [JsonProperty("customerName")]
        public string CustomerName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("manualNumber")]
        public string ManualNumber { get; set; }

        [JsonProperty("createDate")]
        public string CreateDate { get; set; }

        [JsonProperty("prtId")]
        public string PrtId { get; set; }

        [JsonProperty("materialType")]
        public string MaterialType { get; set; }

        [JsonProperty("finalQty")]
        public int FinalQty { get; set; }

        [JsonProperty("deliveryDate")]
        public string DeliveryDate { get; set; }

        [JsonProperty("image")]
        public byte[] Image { get; set; }
    }
}
