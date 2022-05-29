using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Raditap.Api.Misc;

namespace Api.BackOffice
{
    public partial class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
                                                    .AddJsonFile("appsettings.json", true, true) //  We still need appsettings because Logging need to be override
                                                    .AddJsonFile("appsettings.Development.json", true, true)
                                                    .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(options => { options.AllowSynchronousIO = true; });
            services.Configure<IISServerOptions>(options => { options.AllowSynchronousIO = true; });

            services.AddControllers()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                    .ConfigureApiBehaviorOptions(options => { 
                        options.InvalidModelStateResponseFactory = ModelStateValidator.ValidateModelState;
                    })
                    .AddNewtonsoftJson(opt =>
                    {
                        opt.SerializerSettings.DateFormatString = "yyyy-MM-ddTHH:mm:ssZ";
                        opt.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                        opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    });

            services.AddHttpContextAccessor();

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            ConfigureSettings(services);
            ConfigureFilters(services);
            ConfigureDatabase(services);
            ConfigureHelpers(services);
            ConfigureManagers(services);
            ConfigureGacServices(services);
            ConfigureMediatR(services);
            ConfigureJwtAuthentication(services);
            ConfigureCustomerData(services);
            ConfigureValidators(services);
            ConfigureSwagger(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            ConfigureSerilog(app, loggerFactory);
            ConfigureMiddleware(app);

            app.UseCors("MyPolicy");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            ConfigureSwagger(app);
        }
    }
}