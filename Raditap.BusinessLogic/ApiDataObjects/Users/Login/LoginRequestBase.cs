using Raditap.BusinessLogic.ValidationAttributes.PropertyAttributes;
using Newtonsoft.Json;

namespace Raditap.BusinessLogic.ApiDataObjects.Users.Login
{
    public abstract class LoginRequestBase : RequestBase
    {
        [JsonProperty("password")]
        [CustomRequired, CustomMaxLength(20)]
        public string Password { get; set; }
    }
}
