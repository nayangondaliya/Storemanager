using Newtonsoft.Json;

namespace Raditap.BusinessLogic.ApiDataObjects.Jobs
{
    public class PrintJobResponse : ResponseBase<Result>
    {
        public PrintJobResponse(Result result) : base(result)
        {
        }

        [JsonProperty("job")]
        public PrintJobDto Job { get; set; }
    }
}
