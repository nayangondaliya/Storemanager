using Newtonsoft.Json;

namespace Raditap.BusinessLogic.ApiDataObjects.Jobs
{
    public class LastJobNumberResponse : ResponseBase<Result>
    {
        public LastJobNumberResponse(Result result) : base(result)
        {
        }

        [JsonProperty("manualNumber")]
        public string ManualNumber { get; set; }
    }
}
