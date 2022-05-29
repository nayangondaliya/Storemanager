using System;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Raditap.BusinessLogic.ApiDataObjects.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Api.BackOffice.Middlewaress
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger("GlobalException");
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error: {ex}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Response.ContentType = MediaTypeNames.Application.Json;

            return context.Response.WriteAsync(new InternalServerErrorResponse().ToString());
        }
    }
}
