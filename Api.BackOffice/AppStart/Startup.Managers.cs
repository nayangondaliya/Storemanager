using Microsoft.Extensions.DependencyInjection;
using Raditap.BusinessLogic.Interfaces.Managers;
using Raditap.BusinessLogic.Managers;

namespace Api.BackOffice
{
    public partial class Startup
    {
        public void ConfigureManagers(IServiceCollection services)
        {
            services.AddTransient<ILoginManager, LoginManager>();
        }
    }
}
