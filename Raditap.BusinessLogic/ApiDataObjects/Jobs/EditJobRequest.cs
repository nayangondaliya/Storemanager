using MediatR;
using Newtonsoft.Json;
using Raditap.BusinessLogic.ValidationAttributes.PropertyAttributes;
using Raditap.Utilities.Extensions;
using System;

namespace Raditap.BusinessLogic.ApiDataObjects.Jobs
{
    public class EditJobRequest : RequestBase, IRequest<EditJobResponse>
    {
        [CustomRequired, GreaterThanOrEqualTo(1)]
        [JsonProperty("jobId")]
        public long JobId { get; set; }

        [CustomRequired, Between(1, 5)]
        [JsonProperty("statusId")]
        public int StatusId { get; set; }

        [CustomMaxLength(2000), CustomRequired]
        [JsonProperty("customerName")]
        public string CustomerName { get; set; }

        [CustomMaxLength(30), CustomRequired]
        [JsonProperty("manualNumber")]
        public string ManualNumber { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("materialType")]
        public string MaterialType { get; set; }

        [CustomMaxLength(10)]
        [JsonProperty("prtId")]
        public string PrtId { get; set; }

        [JsonProperty("orderQty")]
        public int OrderQty { get; set; }

        [CustomMaxLength(10)]
        [JsonProperty("orderUnit")]
        public string OrderUnit { get; set; }

        [JsonProperty("finalQty")]
        public int FinalQty { get; set; }

        [CustomMaxLength(10)]
        [JsonProperty("finalUnit")]
        public string FinalUnit { get; set; }

        [JsonProperty("delieveryDate")]
        [CustomMaxLength(10), DateTime("yyyy/MM/dd")]
        public string DelieveryDate { get; set; }

        [JsonIgnore]
        public DateTime? DelDate => DelieveryDate.ParseDateTime("yyyy/MM/dd");

        [JsonProperty("billingDate")]
        [CustomMaxLength(10), DateTime("yyyy/MM/dd")]
        public string BillingDate { get; set; }

        [JsonIgnore]
        public DateTime? BillDate => BillingDate.ParseDateTime("yyyy/MM/dd");

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("image")]
        public byte[] Image { get; set; }
    }
}
