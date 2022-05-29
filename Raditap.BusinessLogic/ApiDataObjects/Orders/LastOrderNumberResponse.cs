using Newtonsoft.Json;

namespace Raditap.BusinessLogic.ApiDataObjects.Orders
{
    public class LastOrderNumberResponse : ResponseBase<Result>
    {
        public LastOrderNumberResponse(Result result) : base(result)
        {
        }

        [JsonProperty("manualNumber")]
        public string ManualNumber { get; set; }
    }
}
