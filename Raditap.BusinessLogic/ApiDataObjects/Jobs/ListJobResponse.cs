using Newtonsoft.Json;
using System.Collections.Generic;

namespace Raditap.BusinessLogic.ApiDataObjects.Jobs
{
    public class ListJobResponse : PaginationResponseBase
    {
        public ListJobResponse(Result result) : base(result) { }

        [JsonProperty("jobs")]
        public List<ListJobDto> Jobs { get; set; }
    }
}
