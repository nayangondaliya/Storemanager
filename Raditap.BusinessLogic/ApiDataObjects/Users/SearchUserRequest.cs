using MediatR;
using Newtonsoft.Json;
using Raditap.BusinessLogic.ValidationAttributes.PropertyAttributes;

namespace Raditap.BusinessLogic.ApiDataObjects.Users
{
    public class SearchUserRequest : RequestBase, IRequest<SearchUserResponse>
    {
        [CustomRequired, GreaterThanOrEqualTo(1)]
        [JsonProperty("userId")]
        public long UserId { get; set; }
    }
}
