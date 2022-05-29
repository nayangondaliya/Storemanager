using Newtonsoft.Json;
using System.Collections.Generic;

namespace Raditap.BusinessLogic.ApiDataObjects.Orders
{
    public class ListOrderResponse : PaginationResponseBase
    {
        public ListOrderResponse(Result result) : base(result) { }

        [JsonProperty("orders")]
        public List<ListOrderDto> Orders { get; set; }
    }
}
