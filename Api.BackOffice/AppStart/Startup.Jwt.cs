using System.Net;
using System.Net.Mime;
using Raditap.BusinessLogic.ApiDataObjects.Common;
using Raditap.BusinessLogic.Interfaces.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Raditap.BusinessLogic.Contexts;
using Raditap.DataObjects.AppSettings;
using System.Security.Claims;

namespace Api.BackOffice
{
    public partial class Startup
    {
        public void ConfigureJwtAuthentication(IServiceCollection services)
        {
            var jwtHelper = services.BuildServiceProvider().GetService<IJwtHelper>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                    .AddJwtBearer(x =>
                    {
                        x.TokenValidationParameters = jwtHelper.GetTokenValidationParameters();
                        x.Events = new JwtBearerEvents
                        {
                            OnChallenge = context =>
                            {
                                var factory = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>();
                                var logger = factory.CreateLogger("JwtAuthentication");

                                logger.LogInformation($"Error occured while validating token: {context.Error} - {context.AuthenticateFailure?.Message}");

                                context.HandleResponse();
                                context.Response.StatusCode = (int)HttpStatusCode.OK;
                                context.Response.ContentType = MediaTypeNames.Application.Json;
                                return context.Response.WriteAsync(new InvalidAccessTokenResponse().ToString());
                            }
                        };
                    });

            //  Inject user context (User data in jwt)
            services.AddScoped<UserContext>(p =>
            {
                var httpContext = p.GetRequiredService<IHttpContextAccessor>().HttpContext;
                var appSettings = p.GetRequiredService<AppSettings>();
                
                var claimsPrincipal = httpContext?.User;

                return new UserContext(claimsPrincipal?.Identity as ClaimsIdentity, appSettings);
            });
        }
    }
}
