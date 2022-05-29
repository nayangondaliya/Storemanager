using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Raditap.Api.Misc;
using Raditap.DataObjects.AppSettings;

namespace Api.BackOffice.Filters
{
    /// <summary>
    /// This custom class was created for override response message in case invalid token
    /// </summary>
    public class CustomAuthorizeFilter : IAuthorizationFilter
    {
        private readonly ApiSettings _apiSettings;

        public CustomAuthorizeFilter(ApiSettings apiSettings)
        {
            _apiSettings = apiSettings;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (IsIgnoreTokenRequest(context)) return;

            foreach (var filterDescriptors in context.ActionDescriptor.FilterDescriptors)
            {
                if (filterDescriptors.Filter.GetType() == typeof(AllowAnonymousFilter)) return;
            }

            if (context.HttpContext.User.Identity.IsAuthenticated == false)
            {
                context.Result = new CustomUnauthorizedResult();
            }
        }

        private bool IsIgnoreTokenRequest(AuthorizationFilterContext context)
        {
            foreach (var ignorePath in _apiSettings.IgnoreTokenApiPaths)
            {
                if (string.IsNullOrWhiteSpace(ignorePath)) continue;

                if (context.HttpContext.Request.Path.Value.Replace("api/v1/", "").ToLower().Contains(ignorePath))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
