using Newtonsoft.Json;
using System.Collections.Generic;

namespace Raditap.BusinessLogic.ApiDataObjects.Users
{
    public class ListUserResponse : PaginationResponseBase
    {
        public ListUserResponse(Result result) : base(result) { }

        [JsonProperty("users")]
        public List<ListUserDto> Users { get; set; }
    }
}
