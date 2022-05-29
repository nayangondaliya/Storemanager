using MediatR;
using Newtonsoft.Json;
using Raditap.BusinessLogic.ValidationAttributes.PropertyAttributes;

namespace Raditap.BusinessLogic.ApiDataObjects.Users
{
    public class AddUserRequest : RequestBase, IRequest<AddUserResponse>
    {
        [CustomRequired]
        [CustomMaxLength(50)]
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [CustomMaxLength(50)]
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [CustomRequired]
        [CustomMaxLength(50)]
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [CustomRequired]
        [CustomMaxLength(20)]
        [JsonProperty("password")]
        public string Password { get; set; }

        [CustomMaxLength(10)]
        [JsonProperty("mobileNumber")]
        public string MobileNumber { get; set; }
    }
}
