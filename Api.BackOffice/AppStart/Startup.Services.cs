using Microsoft.Extensions.DependencyInjection;

namespace Api.BackOffice
{
    public partial class Startup
    {
        public void ConfigureGacServices(IServiceCollection services)
        {
            //services.AddScoped<IFirebasePushNotificationService, FirebasePushNotificationService>();

            ////  Notification services
            //services.AddTransient<ICustomerEmailNotificationService, CustomerEmailNotificationService>();
            //services.AddTransient<ITransactionEmailNotificationService, TransactionEmailNotificationService>();
            //services.AddTransient<ITransactionPushNotificationService, TransactionPushNotificationService>();
            //services.AddTransient<IRequestTransactionPushNotificationService, RequestTransactionPushNotificationService>();
        }
    }
}
