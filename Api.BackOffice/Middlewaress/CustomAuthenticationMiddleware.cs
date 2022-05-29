using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Raditap.BusinessLogic.ApiDataObjects.Common;
using Raditap.BusinessLogic.Interfaces.Helpers;
using Raditap.DataObjects.AppSettings;
using Raditap.Utilities.Exceptions;
using Raditap.Utilities.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Api.BackOffice.Middlewaress
{
    public class CustomAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomAuthenticationMiddleware> _logger;
        private readonly IJwtHelper _jwtHelper;
        private readonly ApiSettings _apiSettings;

        public CustomAuthenticationMiddleware(RequestDelegate next,
                                              ILogger<CustomAuthenticationMiddleware> logger,
                                              IJwtHelper jwtHelper,
                                              ApiSettings apiSettings)
        {
            _next = next;
            _logger = logger;
            _jwtHelper = jwtHelper;
            _apiSettings = apiSettings;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                if (IsIgnoreTokenRequest(context))
                {
                    await _next(context);
                    return;
                }

                if (context.Request.Headers.ContainsKey("Authorization"))
                {
                    var authorizationHeader = context.Request.Headers["Authorization"].ToString();
                    if (!string.IsNullOrWhiteSpace(authorizationHeader) && authorizationHeader.StartsWith("bearer", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var token = authorizationHeader.Split(" ");
                        if (token.Length == 2)
                        {
                            var jwtToken = ValidateAndDecode(token[1]);
                        }
                    }
                }
            }
            catch (AesFieldException)
            {
                _logger.LogInformation("Cannot decrypt access token data");
                await HandleResponse(context, new InvalidAccessTokenResponse().ToString());
                return;
            }
            catch (SecurityTokenValidationException stvex)
            {
                // The token failed validation!
                _logger.LogInformation($"Token failed validation: {stvex.Message}");

                //  Expired case
                if (stvex.Message.StartsWith("IDX10223"))
                {
                    await HandleResponse(context, new AccessTokenExpiredResponse().ToString());
                }
                else
                {
                    await HandleResponse(context, new InvalidAccessTokenResponse().ToString());
                }

                return;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while manually decrypting jwt");
                await HandleResponse(context, new InvalidAccessTokenResponse().ToString());
                return;
            }

            await _next(context);
        }

        private JwtSecurityToken ValidateAndDecode(string jwt)
        {
            var validationParameters = _jwtHelper.GetTokenValidationParameters();

            var claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(jwt, validationParameters, out var rawValidatedToken);

            return (JwtSecurityToken)rawValidatedToken;
        }

        private async Task HandleResponse(HttpContext context, string response)
        {
            using (var requestBody = new StreamReader(context.Request.Body))
            {
                var bodyAsText = await requestBody.ReadToEndAsync();

                var encryptedRequest = $"{bodyAsText}";
                _logger.LogInformation($"{context.Request.Method} {context.Request.Path}{context.Request.QueryString} - Pgp Encrypted Request: {encryptedRequest}");

                context.Response.Clear();
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.ContentType = MediaTypeNames.Application.Json;

                _logger.LogWarning("Plain Response: {StatusCode} {ResponseCode} {ResponseDesc} {Response}",
                                   context.Response.StatusCode,
                                   response.TryGetResponseCode(),
                                   response.TryGetResponseDescription(),
                                   response);

                await context.Response.WriteAsync(response);
            }
        }

        private bool IsIgnoreTokenRequest(HttpContext context)
        {
            foreach (var ignorePath in _apiSettings.IgnoreTokenApiPaths)
            {
                if (string.IsNullOrWhiteSpace(ignorePath)) continue;

                if (context.Request.Path.Value.Contains(ignorePath))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
