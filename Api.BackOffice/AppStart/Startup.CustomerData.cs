using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Raditap.BusinessLogic.Contexts;
using Raditap.DataObjects.Constants;

namespace Api.BackOffice
{
    public partial class Startup
    {
        public void ConfigureCustomerData(IServiceCollection services)
        {
            services.AddScoped<LoginUserData>(p =>
            {
                var httpContext = p.GetRequiredService<IHttpContextAccessor>().HttpContext;

                if (httpContext.Request.Headers.ContainsKey(RaditapConstants.InternalCustomerDataHeader))
                {
                    var customerData = httpContext.Request.Headers[RaditapConstants.InternalCustomerDataHeader].ToString();

                    return JsonConvert.DeserializeObject<LoginUserData>(customerData);
                }

                return null;
            });
        }
    }
}
