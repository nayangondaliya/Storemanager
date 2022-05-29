using MediatR;
using Newtonsoft.Json;
using Raditap.BusinessLogic.ValidationAttributes.PropertyAttributes;

namespace Raditap.BusinessLogic.ApiDataObjects.Users
{
    public class ListUserRequest : PaginationRequestBase, IRequest<ListUserResponse>
    {
        [CustomMaxLength(50)]
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [CustomMaxLength(50)]
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [CustomMaxLength(50)]
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [CustomMaxLength(10)]
        [JsonProperty("mobileNumber")]
        public string MobileNumber { get; set; }
    }
}
