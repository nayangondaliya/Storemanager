using MediatR;
using Newtonsoft.Json;
using Raditap.BusinessLogic.ValidationAttributes.PropertyAttributes;

namespace Raditap.BusinessLogic.ApiDataObjects.Users
{
    public class EditUserRequest : RequestBase, IRequest<EditUserResponse>
    {
        [CustomRequired, GreaterThanOrEqualTo(1)]
        [JsonProperty("userId")]
        public long UserId { get; set; }

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

        [CustomExactLength(10)]
        [JsonProperty("mobileNumber")]
        public string MobileNumber { get; set; }
    }
}
