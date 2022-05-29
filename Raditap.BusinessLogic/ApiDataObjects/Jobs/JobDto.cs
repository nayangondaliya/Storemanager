using Newtonsoft.Json;
using System;

namespace Raditap.BusinessLogic.ApiDataObjects.Jobs
{
    public class JobDto
    {
        [JsonProperty("jobId")]
        public long JobId { get; set; }

        [JsonProperty("statusId")]
        public int StatusId { get; set; }

        [JsonProperty("manualNumber")]
        public string ManualNumber { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("materialType")]
        public string MaterialType { get; set; }

        [JsonProperty("prtId")]
        public string PrinterId { get; set; }

        [JsonProperty("orderQty")]
        public int OrderQty { get; set; }

        [JsonProperty("orderUnit")]
        public string OrderUnit { get; set; }

        [JsonProperty("finalQty")]
        public int FinalQty { get; set; }

        [JsonProperty("finalUnit")]
        public string FinalUnit { get; set; }

        [JsonProperty("delieveryDate")]
        public string DelieveryDate { get; set; }

        [JsonProperty("billingDate")]
        public string BillingDate { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("image")]
        public byte[] Image { get; set; }

        [JsonProperty("systemUser")]
        public string SystemUser { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("customerName")]
        public string CustomerName { get; set; }
    }
}
