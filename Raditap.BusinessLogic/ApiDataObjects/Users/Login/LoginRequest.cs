using Raditap.BusinessLogic.ValidationAttributes.PropertyAttributes;
using MediatR;
using Newtonsoft.Json;

namespace Raditap.BusinessLogic.ApiDataObjects.Users.Login
{
    public class LoginRequest : LoginRequestBase, IRequest<LoginResponse>
    {
        [JsonProperty("email")]
        [CustomRequired, CustomMaxLength(50)]
        public string Email { get; set; }
    }
}
