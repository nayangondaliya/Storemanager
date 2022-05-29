using Newtonsoft.Json;

namespace Raditap.BusinessLogic.ApiDataObjects
{
    public abstract class PaginationResponseBase : ResponseBase<Result>
    {
        protected PaginationResponseBase(Result result) : base(result) { }

        [JsonProperty("pageInfo")]
        public PageInfo PageInfo { get; set; }
    }
}
