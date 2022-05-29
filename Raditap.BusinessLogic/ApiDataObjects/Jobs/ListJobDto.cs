using Newtonsoft.Json;
using System;

namespace Raditap.BusinessLogic.ApiDataObjects.Jobs
{
    public class ListJobDto
    {
        [JsonProperty("jobId")]
        public long JobId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("customerName")]
        public string CustomerName { get; set; }

        [JsonProperty("manualNumber")]
        public string ManualNumber { get; set; }

        [JsonProperty("prtId")]
        public string PrinterId { get; set; }

        [JsonProperty("deliveryDate")]
        public string DeliveryDate { get; set; }

        [JsonProperty("billingDate")]
        public string BillingDate { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
