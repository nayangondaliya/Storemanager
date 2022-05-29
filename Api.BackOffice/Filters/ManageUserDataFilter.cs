using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Raditap.Api.Misc;
using Raditap.BusinessLogic.Contexts;
using Raditap.DataObjects.Constants;

namespace Api.BackOffice.Filters
{
    public class ManageUserDataFilter : ActionFilterAttribute
    {
        private readonly ILogger<ManageUserDataFilter> _logger;
        private readonly UserContext _userContext;
        
        public ManageUserDataFilter(ILogger<ManageUserDataFilter> logger, UserContext userContext)
        {
            _logger = logger;
            _userContext = userContext;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (_userContext?.CustomerData == null)
            {
                _logger.LogInformation("Customer data is empty or invalid");
                context.Result = new CustomUnauthorizedResult();
                return;
            }
        }
    }
}
