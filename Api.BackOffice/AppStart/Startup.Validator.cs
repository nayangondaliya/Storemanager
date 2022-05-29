using System.Reflection;
using Raditap.BusinessLogic.Interfaces.Validations;
using Microsoft.Extensions.DependencyInjection;

namespace Api.BackOffice
{
    /// <summary>
    /// Source: https://stackoverflow.com/questions/39320265/how-to-inject-dependencies-of-generics-in-asp-net-core
    /// </summary>
    public partial class Startup
    {
        public void ConfigureValidators(IServiceCollection services)
        {
            //services.AddTransient(typeof(IRequestValidator<,>), typeof(RegisterCustomerRequestValidator));
            //services.AddClassesAsImplementedInterface(Assembly.GetEntryAssembly(), typeof(IRequestValidator<,>), ServiceLifetime.Transient);
            //services.AddClassesAsImplementedInterface(typeof(RequestValidatorBase).Assembly, typeof(IRequestValidator<,>), ServiceLifetime.Transient);

            //services.AddSingleton<WalletLimitValidator>();
        }
    }
}
