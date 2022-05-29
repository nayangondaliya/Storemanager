using MediatR;
using Newtonsoft.Json;
using Raditap.BusinessLogic.ValidationAttributes.PropertyAttributes;

namespace Raditap.BusinessLogic.ApiDataObjects.Orders
{
    public class EditOrderRequest : RequestBase, IRequest<EditOrderResponse>
    {
        [CustomRequired, GreaterThanOrEqualTo(1)]
        [JsonProperty("orderId")]
        public long OrderId { get; set; }

        [CustomMaxLength(20)]
        [JsonProperty("manualNumber")]
        public string ManualNumber { get; set; }

        [CustomRequired, Between(1, 5)]
        [JsonProperty("statusId")]
        public int StatusId { get; set; }

        [CustomRequired]
        [CustomMaxLength(255)]
        [JsonProperty("customerName")]
        public string CustomerName { get; set; }

        [CustomExactLength(10)]
        [JsonProperty("customerMobileNumber")]
        public string CustomerMobileNumber { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
