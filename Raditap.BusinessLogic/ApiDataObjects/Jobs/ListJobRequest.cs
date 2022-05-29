using MediatR;
using Newtonsoft.Json;
using Raditap.BusinessLogic.ValidationAttributes.PropertyAttributes;
using Raditap.Utilities.Extensions;
using System;

namespace Raditap.BusinessLogic.ApiDataObjects.Jobs
{
    public class ListJobRequest : PaginationRequestBase, IRequest<ListJobResponse>
    {
        [CustomMaxLength(2000)]
        [JsonProperty("customerName")]
        public string CustomerName { get; set; }

        [CustomMaxLength(30)]
        [JsonProperty("manualNumber")]
        public string ManualNumber { get; set; }

        [Between(0, 5)]
        [JsonProperty("statusId")]
        public int StatusId { get; set; }

        [CustomMaxLength(10)]
        [JsonProperty("prtId")]
        public string PrtId { get; set; }

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
    }
}
