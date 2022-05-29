using System.Net;
using Raditap.BusinessLogic.ApiDataObjects.Common;
using Microsoft.AspNetCore.Mvc;

namespace Raditap.Api.Misc
{
    public class InvalidRequestResult : JsonResult
    {
        public InvalidRequestResult() : base(new InvalidRequestResponse())
        {
            StatusCode = (int)HttpStatusCode.OK;
        }
    }
}