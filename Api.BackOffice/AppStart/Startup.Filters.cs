using Api.BackOffice.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Api.BackOffice
{
    public partial class Startup
    {
        public void ConfigureFilters(IServiceCollection services)
        {
            services.AddScoped<RequestResponseLoggingFilter>();
            services.AddScoped<CustomAuthorizeFilter>();
            //services.AddScoped<ManageUserDataFilter>();
            //services.AddScoped<HandshakeValidationFilter>();
        }
    }
}
