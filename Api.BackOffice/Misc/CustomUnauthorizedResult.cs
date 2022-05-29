using System.Net;
using Raditap.BusinessLogic.ApiDataObjects.Common;
using Microsoft.AspNetCore.Mvc;

namespace Raditap.Api.Misc
{
    public class CustomUnauthorizedResult : JsonResult
    {
        public CustomUnauthorizedResult() : base(new UnauthorizedResponse())
        {
            StatusCode = (int)HttpStatusCode.OK;
        }
    }
}