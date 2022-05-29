using Raditap.BusinessLogic.ValidationAttributes.PropertyAttributes;
using Newtonsoft.Json;

namespace Raditap.BusinessLogic.ApiDataObjects
{
    public abstract class PaginationRequestBase : RequestBase
    {
        [JsonProperty("pageNo")]
        [GreaterThanOrEqualTo(1), Between(1, 100)]
        public int PageNo { get; set; }

        [JsonProperty("pageSize")]
        [Between(1, 100)]
        public int PageSize { get; set; }

        public int GetSkip()
        {
            return (PageNo - 1) * PageSize;
        }
    }
}
