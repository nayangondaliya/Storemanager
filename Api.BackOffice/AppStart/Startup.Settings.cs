using Api.BackOffice.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Raditap.DataObjects.AppSettings;

namespace Api.BackOffice
{
    public partial class Startup
    {
        public void ConfigureSettings(IServiceCollection services)
        {
            services.AddSettingsSingleton<AppSettings>(Configuration, "AppSettings");
            services.AddSettingsSingleton<DatabaseSettings>(Configuration, "DatabaseSettings");
            services.AddSettingsSingleton<TokenSettings>(Configuration, "Token");
            services.AddSettingsSingleton<MaskingSettings>(Configuration, "MaskingSettings");
            services.AddSettingsSingleton<FormatSettings>(Configuration, "FormatSettings");
            services.AddSettingsSingleton<ApiSettings>(Configuration, "ApiSettings");
        }
    }
}
