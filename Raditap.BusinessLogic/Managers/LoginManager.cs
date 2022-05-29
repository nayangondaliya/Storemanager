using System;
using System.Threading.Tasks;
using Raditap.BusinessLogic.ApiDataObjects;
using Raditap.BusinessLogic.ApiDataObjects.Users.Login;
using Raditap.BusinessLogic.Contexts;
using Raditap.BusinessLogic.Interfaces.Managers;
using Raditap.DatabaseAccess.Entities;
using Raditap.DatabaseAccess.Interfaces.Repositories;
using Raditap.DataObjects.AppSettings;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Raditap.Utilities.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Raditap.BusinessLogic.Managers
{
    public class LoginManager : ILoginManager
    {
        private readonly ILogger<LoginManager> _logger;
        private readonly IUserRepository _userRepository;
        private readonly AppSettings _appSettings;
        private readonly DatabaseSettings _databaseSettings;
        private readonly TokenSettings _tokenSettings;
        
        public LoginManager(ILogger<LoginManager> logger,
                            IUserRepository userRepository,
                            AppSettings appSettings,
                            DatabaseSettings databaseSettings,
                            TokenSettings tokenSettings)
        {
            _logger = logger;
            _appSettings = appSettings;
            _databaseSettings = databaseSettings;
            _userRepository = userRepository;
            _tokenSettings = tokenSettings;
        }

        public async Task<(Result result, string userData)> Handle(LoginRequestBase request, User user)
        {
           
            if (user.DecryptLoginPasscode(user.Passcode, _databaseSettings.AesKey, _databaseSettings.AesIv) != request.Password)
            {
                _logger.LogInformation($"CustomerId: {user.Id} passcode is mismatched");

                user.LastUpdated = DateTime.UtcNow;
                await _userRepository.UpdateAsync(user);
                return (Result.Fail, string.Empty);
            }

            user.LastUpdated = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);
            _logger.LogDebug($"Updated customer login info CustomerId: {user.Id}");

            var loginUserData = new LoginUserData
            {
                Id = user.Id,
                MobileNumber = user.MobileNumber,
                Email = user.Email
            };

            var userData = AesCryptography.Encrypt(JsonConvert.SerializeObject(loginUserData), _appSettings.AesKey, _appSettings.AesIv);

            return (Result.Success, userData);
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_tokenSettings.SecurityKey);

            var userData = JsonConvert.SerializeObject(new LoginUserData() { Email = user.Email, FirstName = user.FirstName, Id = user.Id, LastName = user.LastName, MobileNumber = user.MobileNumber});
            var encryptedUserData = AesCryptography.Encrypt(userData, _appSettings.AesKey, _appSettings.AesIv);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.UserData, encryptedUserData) }),
                Expires = DateTime.UtcNow.AddMinutes(_tokenSettings.ExpiresInMinutes),
                Issuer = _tokenSettings.Issuer,
                Audience = _tokenSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
