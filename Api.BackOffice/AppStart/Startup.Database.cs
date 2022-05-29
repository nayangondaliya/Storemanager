using Raditap.DatabaseAccess.EfRepositories;
using Raditap.DatabaseAccess.Entities;
using Raditap.DatabaseAccess.Interfaces;
using Raditap.DatabaseAccess.Interfaces.Repositories;
using Raditap.DatabaseAccess.Repositories;
using Raditap.DataObjects.AppSettings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Api.BackOffice
{
    public partial class Startup
    {
        public void ConfigureDatabase(IServiceCollection services)
        {
            //var lf = new LoggerFactory();
            //lf.AddProvider(new DbLoggerProvider());

            var dbSettings = services.BuildServiceProvider().GetService<DatabaseSettings>();

            //services.AddEntityFrameworkMySql();

            //  NOTE: Since 2020-03-10 .net can't accept more than one db pool so context will be registered as db context instead of db context pool
            services.AddDbContext<RaditapContext>(options =>
            {
                options.UseSqlServer(dbSettings.Connection, mysqlOptions => { mysqlOptions.CommandTimeout(dbSettings.TimeoutInSeconds); });
                //.UseLoggerFactory(lf);
            },
            ServiceLifetime.Transient);

            services.AddDbContext<ReadRaditapContext>(options =>
            {
                options.UseSqlServer(dbSettings.ReadOnlyConnection,
                                 mysqlOptions => { mysqlOptions.CommandTimeout(dbSettings.TimeoutInSeconds); });
                //.UseLoggerFactory(lf);
            },
                                                  ServiceLifetime.Transient);

            services.AddTransient(typeof(IAsyncRepository<>), typeof(EfRepository<>));

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IJobRepository, JobRepository>();
        }
    }
}
