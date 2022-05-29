using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Raditap.BusinessLogic.Handlers.Authentication;

namespace Api.BackOffice
{
    public partial class Startup
    {
        public void ConfigureMediatR(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));
            services.AddMediatR(typeof(LoginHandler).Assembly);
        }
    }
}
