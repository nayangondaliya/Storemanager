using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Raditap.BusinessLogic.Contexts;
using Raditap.DataObjects.AppSettings;
using Raditap.DataObjects.Constants;
using Raditap.Utilities.Cryptography;
using Serilog.Context;
using System;
using System.Threading.Tasks;

namespace Api.BackOffice.Middlewaress
{
    public class ManageHeaderPropertyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ManageHeaderPropertyMiddleware> _logger;
        private readonly AppSettings _appSettings;

        public ManageHeaderPropertyMiddleware(RequestDelegate next,
                                              ILogger<ManageHeaderPropertyMiddleware> logger,
                                              AppSettings appSettings)
        {
            _next = next;
            _logger = logger;
            _appSettings = appSettings;
        }

        public async Task Invoke(HttpContext context)
        {
            ManageCustomerData(context);

            await _next(context);
        }

        private void ManageCustomerData(HttpContext context)
        {
            try
            {
                if (context.Request.Headers.ContainsKey(RaditapConstants.CustomerDataHeader))
                {
                    var customerData = context.Request.Headers[RaditapConstants.CustomerDataHeader].ToString();

                    var decryptedCustomerData = AesCryptography.Decrypt(customerData, _appSettings.AesKey, _appSettings.AesIv);

                    var userData = JsonConvert.DeserializeObject<LoginUserData>(decryptedCustomerData);

                    LogContext.PushProperty(RaditapConstants.RequestedCustomerEmail, userData?.Email);
                    LogContext.PushProperty(RaditapConstants.RequestedCustomerMobileNumber, userData?.MobileNumber);
                    LogContext.PushProperty(RaditapConstants.RequestedCustomerId, userData?.Id);

                    context.Request.Headers.Add(RaditapConstants.InternalCustomerDataHeader, decryptedCustomerData);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while manually decrypting customer data from header");
            }
        }
    }
}
