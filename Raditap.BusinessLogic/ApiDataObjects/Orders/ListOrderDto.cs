using Newtonsoft.Json;

namespace Raditap.BusinessLogic.ApiDataObjects.Orders
{
    public class ListOrderDto
    {
        [JsonProperty("orderId")]
        public long OrderId { get; set; }

        [JsonProperty("manualNumber")]
        public string ManualNumber { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
        
        [JsonProperty("customerName")]
        public string CustomerName { get; set; }

        [JsonProperty("customerMobileNumber")]
        public string CustomerMobileNumber { get; set; }
    }
}
