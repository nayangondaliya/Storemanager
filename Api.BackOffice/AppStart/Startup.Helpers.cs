using Microsoft.Extensions.DependencyInjection;
using Raditap.BusinessLogic.Helpers;
using Raditap.BusinessLogic.Interfaces.Helpers;
using Raditap.Utilities.Helpers;

namespace Api.BackOffice
{
    public partial class Startup
    {
        public void ConfigureHelpers(IServiceCollection services)
        {
            //services.AddTransient<IAmazonHelper, AmazonHelper>();
            services.AddTransient<IJwtHelper, JwtHelper>();
            services.AddTransient<IFileOperationHelper, FileOperationHelper>();
            services.AddTransient<ProcessingTimeHelper, ProcessingTimeHelper>();
        }
    }
}
