using System;
using System.Net;
using Raditap.BusinessLogic.ApiDataObjects;
using Raditap.BusinessLogic.ApiDataObjects.Common;
using Raditap.DataObjects.AppSettings;
using Raditap.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Raditap.Api.Misc;

namespace Api.BackOffice.Filters
{
    public class RequestResponseLoggingFilter : ActionFilterAttribute
    {
        private readonly ILogger _logger;
        private readonly MaskingSettings _maskingSettings;
        private readonly ApiSettings _apiSettings;

        public RequestResponseLoggingFilter(ILoggerFactory loggerFactory, MaskingSettings maskingSettings, ApiSettings apiSettings)
        {
            _logger = loggerFactory.CreateLogger("LogFilter");
            _maskingSettings = maskingSettings;
            _apiSettings = apiSettings;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var request = FormatRequest(context);

            _logger.LogInformation($"Plain Request: {request}");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var response = string.Empty;
            int? statusCode = 0;
            var responseCode = string.Empty;
            var responseDesc = string.Empty;

            switch (context.Result)
            {
                case ObjectResult objResult:

                    var res = JsonConvert.SerializeObject(objResult.Value);
                    statusCode = objResult.StatusCode;
                    responseCode = res.TryGetResponseCode();
                    responseDesc = res.TryGetResponseDescription();
                    response = res;

                    break;

                case InvalidRequestResult invalidResult:

                    statusCode = invalidResult.StatusCode;
                    responseCode = Result.InvalidRequest.Value;
                    responseDesc = Result.InvalidRequest.Name;
                    response = Convert.ToString(invalidResult.Value);

                    break;
            }

            //  If found exception, we will log system error response
            if (context.Exception != null && !context.ExceptionHandled)
            {
                _logger.LogCritical(context.Exception, "Global exception");

                response = new InternalServerErrorResponse().ToString();
                responseCode = new InternalServerErrorResponse().Code;
                responseDesc = new InternalServerErrorResponse().Description;
                statusCode = (int)HttpStatusCode.OK;
            }

            if (context.HttpContext.Request.Path.Value.Contains("oauth2/token") && response.Contains("access_token") && response.Contains("expires_in"))
            {
                responseCode = Result.Success.Value;
                responseDesc = Result.Success.Name;
            }

            _logger.LogWarning("Plain Response: {StatusCode} {ResponseCode} {ResponseDesc} {Response}", statusCode, responseCode, responseDesc, response);
        }

        private string FormatRequest(ActionExecutingContext context)
        {
            var requestBody = context.ActionArguments.ContainsKey("request") ? JsonConvert.SerializeObject(context.ActionArguments["request"]) : string.Empty;

            var result = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}{context.HttpContext.Request.QueryString} {requestBody}";

            return result;
        }
    }
}
