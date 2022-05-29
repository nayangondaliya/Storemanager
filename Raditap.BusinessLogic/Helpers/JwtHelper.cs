using System.Text;
using Raditap.BusinessLogic.Interfaces;
using Raditap.BusinessLogic.Interfaces.Helpers;
using Raditap.DataObjects.AppSettings;
using Microsoft.IdentityModel.Tokens;

namespace Raditap.BusinessLogic.Helpers
{
    public class JwtHelper : IJwtHelper
    {
        private readonly AppSettings _appSettings;
        private readonly TokenSettings _tokenSettings;

        public JwtHelper(AppSettings appSettings, TokenSettings tokenSettings)
        {
            _appSettings = appSettings;
            _tokenSettings = tokenSettings;
        }

        public TokenValidationParameters GetTokenValidationParameters()
        {
            var result = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenSettings.SecurityKey)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = _tokenSettings.Issuer,
                ValidAudience = _tokenSettings.Audience
            };

            return result;
        }
    }
}
