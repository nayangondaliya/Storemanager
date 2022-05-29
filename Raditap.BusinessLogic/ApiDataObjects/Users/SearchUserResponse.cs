using Newtonsoft.Json;

namespace Raditap.BusinessLogic.ApiDataObjects.Users
{
    public class SearchUserResponse : ResponseBase<Result>
    {
        public SearchUserResponse(Result result) : base(result)
        {
        }
        
        [JsonProperty("user")]
        public UserDto User { get; set; }
    }
}
