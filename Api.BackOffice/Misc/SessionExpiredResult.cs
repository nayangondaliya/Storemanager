using System.Net;
using Raditap.BusinessLogic.ApiDataObjects.Common;
using Microsoft.AspNetCore.Mvc;

namespace Raditap.Api.Misc
{
    public class SessionExpiredResult : JsonResult
    {
        public SessionExpiredResult() : base(new SessionExpiredResponse())
        {
            StatusCode = (int)HttpStatusCode.OK;
        }
    }
}