using Newtonsoft.Json;

namespace Raditap.BusinessLogic.ApiDataObjects.Users.Login
{
    public class LoginUserDto
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }
    }
}
