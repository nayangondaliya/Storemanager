using Newtonsoft.Json;

namespace Raditap.BusinessLogic.ApiDataObjects.Jobs
{
    public class SearchJobResponse : ResponseBase<Result>
    {
        public SearchJobResponse(Result result) : base(result)
        {   
        }

        [JsonProperty("job")]
        public JobDto Job { get; set; }
    }
}
