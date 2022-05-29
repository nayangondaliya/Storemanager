using Api.BackOffice.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Api.BackOffice.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(RequestResponseLoggingFilter), Order = 1)]
    [ServiceFilter(typeof(CustomAuthorizeFilter), Order = 2)]
    //[ServiceFilter(typeof(ManageUserDataFilter), Order = 2)]
    public class BaseController : ControllerBase
    {
    }
}
