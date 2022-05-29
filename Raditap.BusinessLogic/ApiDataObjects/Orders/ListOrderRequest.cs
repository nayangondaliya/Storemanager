using MediatR;
using Newtonsoft.Json;
using Raditap.BusinessLogic.ValidationAttributes.PropertyAttributes;

namespace Raditap.BusinessLogic.ApiDataObjects.Orders
{
    public class ListOrderRequest : PaginationRequestBase, IRequest<ListOrderResponse>
    {
        [CustomMaxLength(20)]
        [JsonProperty("manualNumber")]
        public string ManualNumber { get; set; }

        [JsonProperty("statusId")]
        public int StatusId { get; set; }

        [CustomMaxLength(255)]
        [JsonProperty("customerName")]
        public string CustomerName { get; set; }

        [CustomMaxLength(10)]
        [JsonProperty("customerMobileNumber")]
        public string CustomerMobileNumber { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
