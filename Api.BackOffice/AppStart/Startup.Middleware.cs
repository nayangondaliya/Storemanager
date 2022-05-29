using Api.BackOffice.Middlewaress;
using Microsoft.AspNetCore.Builder;

namespace Api.BackOffice
{
    public partial class Startup
    {
        public void ConfigureMiddleware(IApplicationBuilder app)
        {
            // Middleware branching https://www.devtrends.co.uk/blog/conditional-middleware-based-on-request-in-asp.net-core

            app.UseMiddleware<ResponseTimeMiddleware>();
            app.UseMiddleware<CustomAuthenticationMiddleware>();
            app.UseMiddleware<ManageHeaderPropertyMiddleware>();
            //app.UseMiddleware<MessageCryptographyMiddleware>();
        }
    }
}
