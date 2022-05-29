using System;
using Raditap.DataObjects.AppSettings;
using Raditap.Utilities.Helpers;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Api.BackOffice.Logging
{
    /// <summary>
    /// Source: https://www.stevejgordon.co.uk/httpclientfactory-asp-net-core-logging
    /// </summary>
    public class CustomHttpClientLoggingFilter : IHttpMessageHandlerBuilderFilter
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ApiSettings _apiSettings;
        private readonly MaskingSettings _maskingSettings;
        private readonly ProcessingTimeHelper _processingTimeHelper;

        public CustomHttpClientLoggingFilter(ILoggerFactory loggerFactory,
                                             ApiSettings apiSettings,
                                             MaskingSettings maskingSettings,
                                             ProcessingTimeHelper processingTimeHelper)
        {
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            _apiSettings = apiSettings;
            _maskingSettings = maskingSettings;
            _processingTimeHelper = processingTimeHelper;
        }

        public Action<HttpMessageHandlerBuilder> Configure(Action<HttpMessageHandlerBuilder> next)
        {
            if (next == null) throw new ArgumentNullException(nameof(next));

            return (builder) =>
            {
                // Run other configuration first, we want to decorate.
                next(builder);

                var outerLogger = _loggerFactory.CreateLogger($"System.Net.Http.HttpClient.{builder.Name}.LogicalHandler");

                if (IsDebug(builder.Name))
                {
                    builder.AdditionalHandlers.Insert(0,
                                                      new CustomLoggingScopeHttpMessageHandler(outerLogger,
                                                                                               false,
                                                                                               _maskingSettings,
                                                                                               _processingTimeHelper,
                                                                                               builder.Name));
                }
                else
                {
                    builder.AdditionalHandlers.Insert(0,
                                                      new CustomLoggingScopeHttpMessageHandler(outerLogger, true, _maskingSettings, _processingTimeHelper, builder.Name));
                }
            };
        }

        private bool IsDebug(string builderName)
        {
            foreach (var clientName in _apiSettings.NotLogMessageHttpClientNames)
            {
                if (string.IsNullOrWhiteSpace(clientName)) continue;

                if (builderName == clientName)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
