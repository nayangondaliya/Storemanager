using System.Collections.Generic;
using Raditap.DataObjects.AppSettings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Api.BackOffice
{
    public partial class Startup
    {
        public void ConfigureSwagger(IServiceCollection services)
        {
            if (!IsEnableSwagger()) return;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Raditap Api", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement(){
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                            Type = ReferenceType.SecurityScheme,
                                            Id = "Bearer"
                                        },
                            Scheme = "JWT",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
        }

        public void ConfigureSwagger(IApplicationBuilder app)
        {
            if (!IsEnableSwagger()) return;

            app.UseSwagger();

#if DEBUG
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GAC Api V1");
                c.DefaultModelExpandDepth(0);
                c.DefaultModelsExpandDepth(-1);
            });
#else
            app.UseSwaggerUI(c =>
                             {
                                 c.SwaggerEndpoint("../swagger/v1/swagger.json", "GAC Api V1");
                                 c.DefaultModelExpandDepth(0);
                                 c.DefaultModelsExpandDepth(-1);
                             });
#endif
        }

        private bool IsEnableSwagger()
        {
            var settings = Configuration.GetSection("ApiSettings").Get<ApiSettings>();
            return settings.EnableSwagger;
        }
    }
}
