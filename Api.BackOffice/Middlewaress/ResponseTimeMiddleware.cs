using Raditap.DataObjects.AppSettings;
using Raditap.DataObjects.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System.Diagnostics;
using System.Threading.Tasks;
using System;

namespace Api.BackOffice.Middlewaress
{
    public class ResponseTimeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly ApiSettings _apiSettings;

        public ResponseTimeMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, ApiSettings apiSettings)
        {
            _next = next;
            _apiSettings = apiSettings;
            _logger = loggerFactory.CreateLogger("ResponseTimeMiddleware");
        }

        public async Task Invoke(HttpContext context)
        {
            LogContext.PushProperty(RaditapConstants.ApiName, context.Request.Path);
            LogContext.PushProperty(RaditapConstants.ApplicationName, "GAC-Api");
            LogContext.PushProperty(RaditapConstants.EnvironmentName, Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));

            var sw = new Stopwatch();
            sw.Start();
            await _next(context);
            sw.Stop();

            _logger.LogWarning("Processing time: {ApiName} {StatusCode} {ElapsedTime} seconds",
                               context.Request.Path,
                               context.Response.StatusCode,
                               sw.Elapsed.TotalSeconds);
        }
    }
}
