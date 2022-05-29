using MediatR;
using Newtonsoft.Json;
using Raditap.BusinessLogic.ValidationAttributes.PropertyAttributes;

namespace Raditap.BusinessLogic.ApiDataObjects.Jobs
{
    public class PrintJobRequest : RequestBase, IRequest<PrintJobResponse>
    {
        [CustomRequired, GreaterThanOrEqualTo(1)]
        [JsonProperty("jobId")]
        public long JobId { get; set; }
    }
}
