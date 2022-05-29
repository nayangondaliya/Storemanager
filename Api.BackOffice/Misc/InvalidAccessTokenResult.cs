using System.Net;
using Raditap.BusinessLogic.ApiDataObjects.Common;
using Microsoft.AspNetCore.Mvc;

namespace Raditap.Api.Misc
{
    public class InvalidAccessTokenResult: JsonResult
    {
        public InvalidAccessTokenResult() : base(new InvalidAccessTokenResponse())
        {
            StatusCode = (int)HttpStatusCode.OK;
        }
    }
}