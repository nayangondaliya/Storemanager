using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Context;

namespace Api.BackOffice
{
    public partial class Startup
    {
        public void ConfigureSerilog(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
            var logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration)
                                                  .Enrich.FromLogContext()
                                                  //.WriteTo.AWSSeriLog(Configuration, textFormatter: new AwsCloudwatchTextFormatter())
                                                  .CreateLogger();

            app.Use(async (context, next) =>
            {
                LogContext.PushProperty("RequestId", context.TraceIdentifier);
                await next();
            });

            loggerFactory.AddSerilog(logger);
        }
    }
}
