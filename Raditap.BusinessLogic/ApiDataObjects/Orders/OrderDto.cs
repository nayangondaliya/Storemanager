using Newtonsoft.Json;
using System;

namespace Raditap.BusinessLogic.ApiDataObjects.Orders
{
    public class OrderDto
    {
        [JsonProperty("orderId")]
        public long OrderId { get; set; }

        [JsonProperty("manualNumber")]
        public string ManualNumber { get; set; }

        [JsonProperty("statusId")]
        public int StatusId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("customerName")]
        public string CustomerName { get; set; }

        [JsonProperty("customerMobileNumber")]
        public string CustomerMobileNumber { get; set; }

        [JsonProperty("systemUser")]
        public string SystemUser { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }
    }
}
