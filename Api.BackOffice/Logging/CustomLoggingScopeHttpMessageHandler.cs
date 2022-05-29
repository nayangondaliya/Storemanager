using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Raditap.DataObjects.AppSettings;
using Raditap.Utilities.Helpers;
using Microsoft.Extensions.Logging;

namespace Api.BackOffice.Logging
{
    /// <summary>
    /// Source: https://www.stevejgordon.co.uk/httpclientfactory-asp-net-core-logging
    /// </summary>
    public class CustomLoggingScopeHttpMessageHandler : DelegatingHandler
    {
        private readonly ILogger _logger;
        private readonly bool _logMessage;
        private readonly MaskingSettings _maskingSettings;
        private readonly ProcessingTimeHelper _processingTimeHelper;
        private readonly string _builderName;

        public CustomLoggingScopeHttpMessageHandler(ILogger logger,
                                                    bool logMessage,
                                                    MaskingSettings maskingSettings,
                                                    ProcessingTimeHelper processingTimeHelper,
                                                    string builderName)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logMessage = logMessage;
            _maskingSettings = maskingSettings;
            _processingTimeHelper = processingTimeHelper;
            _builderName = builderName;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            if (!Enum.TryParse(_builderName, out MeasureOn measureOn))
            {
                measureOn = MeasureOn.ExternalService;
            }
            _processingTimeHelper.StartLogging(measureOn, request.RequestUri.ToString(), request.Method.Method);

            await Log.RequestPipelineStart(_logger, request, _logMessage, _maskingSettings);
            var response = await base.SendAsync(request, cancellationToken);

            await Log.RequestPipelineEnd(_logger, response, _logMessage, _maskingSettings);

            _processingTimeHelper.StopLogging();

            return response;
        }

        private static class Log
        {
            private static class EventIds
            {
                public static readonly EventId PipelineStart = new EventId(100, "RequestPipelineStart");
                public static readonly EventId PipelineEnd = new EventId(101, "RequestPipelineEnd");
            }

            private static string logRequestMessage = "Start processing HTTP request {HttpMethod} {Uri} - message: {Message}";
            private static string logResponseMessage = "End processing HTTP request - {ExternalStatusCode} - message: {Message}";

            private static string logRequestWithoutMessage = "Start processing HTTP request {HttpMethod} {Uri}";
            private static string logResponseWithoutMessage = "End processing HTTP request - {ExternalStatusCode}";

            private static readonly Action<ILogger, HttpMethod, Uri, string, Exception> _requestPipelineStart =
                    LoggerMessage.Define<HttpMethod, Uri, string>(LogLevel.Information, EventIds.PipelineStart, logRequestMessage);

            private static readonly Action<ILogger, HttpStatusCode, string, Exception> _requestPipelineEnd =
                    LoggerMessage.Define<HttpStatusCode, string>(LogLevel.Warning, EventIds.PipelineEnd, logResponseMessage);

            private static readonly Action<ILogger, HttpMethod, Uri, Exception> _requestPipelineWithoutMessageStart =
                    LoggerMessage.Define<HttpMethod, Uri>(LogLevel.Information, EventIds.PipelineStart, logRequestWithoutMessage);

            private static readonly Action<ILogger, HttpStatusCode, Exception> _requestPipelineWithoutMessageEnd =
                    LoggerMessage.Define<HttpStatusCode>(LogLevel.Warning, EventIds.PipelineEnd, logResponseWithoutMessage);

            public static async Task RequestPipelineStart(ILogger logger, HttpRequestMessage request, bool logMessage, MaskingSettings maskingSettings)
            {
                if (logMessage)
                {
                    var requestMessage = await request.Content.ReadAsStringAsync();
                    _requestPipelineStart(logger, request.Method, request.RequestUri, requestMessage, null);
                }
                else

                {
                    _requestPipelineWithoutMessageStart(logger, request.Method, request.RequestUri, null);
                }
            }

            public static async Task RequestPipelineEnd(ILogger logger, HttpResponseMessage response, bool logMessage, MaskingSettings maskingSettings)
            {
                if (logMessage)
                {
                    var responseMessage = await response.Content.ReadAsStringAsync();
                    _requestPipelineEnd(logger, response.StatusCode, responseMessage, null);
                }
                else
                {
                    _requestPipelineWithoutMessageEnd(logger, response.StatusCode, null);
                }
            }
        }
    }
}
