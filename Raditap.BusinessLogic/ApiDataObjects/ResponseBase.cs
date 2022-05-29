using Newtonsoft.Json;

namespace Raditap.BusinessLogic.ApiDataObjects
{
    public abstract class ResponseBase<T> where T : Result
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Description { get; set; }

        protected ResponseBase(T result)
        {
            Code = result.Value;
            Description = result.Name;
        }

        protected ResponseBase(string responseCode, string responseDescription)
        {
            Code = responseCode;
            Description = responseDescription;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
