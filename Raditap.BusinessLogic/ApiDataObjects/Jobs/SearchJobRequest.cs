using MediatR;
using Newtonsoft.Json;
using Raditap.BusinessLogic.ValidationAttributes.PropertyAttributes;

namespace Raditap.BusinessLogic.ApiDataObjects.Jobs
{
    public class SearchJobRequest : RequestBase, IRequest<SearchJobResponse>
    {
        [CustomRequired, GreaterThanOrEqualTo(1)]
        [JsonProperty("jobId")]
        public long JobId { get; set; }
    }
}
