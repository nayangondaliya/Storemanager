using Microsoft.IdentityModel.Tokens;

namespace Raditap.BusinessLogic.Interfaces.Helpers
{
    public interface IJwtHelper
    {
        TokenValidationParameters GetTokenValidationParameters();
    }
}
