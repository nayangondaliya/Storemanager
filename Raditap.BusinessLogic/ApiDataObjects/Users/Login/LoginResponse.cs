using Newtonsoft.Json;

namespace Raditap.BusinessLogic.ApiDataObjects.Users.Login
{
    public class LoginResponse : ResponseBase<Result>
    {
        public LoginResponse(Result result) : base(result) { }

        [JsonProperty("data")]
        public LoginUserDto UserData { get; set; }
    }
}
