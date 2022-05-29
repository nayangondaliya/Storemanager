using Newtonsoft.Json;

namespace Raditap.BusinessLogic.ApiDataObjects.Orders
{
    public class SearchOrderResponse : ResponseBase<Result>
    {
        public SearchOrderResponse(Result result) : base(result)
        {
        }

        [JsonProperty("order")]
        public OrderDto Order { get; set; }
    }
}
