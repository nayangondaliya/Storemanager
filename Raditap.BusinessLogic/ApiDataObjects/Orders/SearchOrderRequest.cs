using MediatR;
using Newtonsoft.Json;
using Raditap.BusinessLogic.ValidationAttributes.PropertyAttributes;

namespace Raditap.BusinessLogic.ApiDataObjects.Orders
{
    public class SearchOrderRequest : RequestBase, IRequest<SearchOrderResponse>
    {
        [CustomRequired, GreaterThanOrEqualTo(1)]
        [JsonProperty("orderId")]
        public long OrderId { get; set; }
    }
}
