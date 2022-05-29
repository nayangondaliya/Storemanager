using MediatR;
using Microsoft.Extensions.Logging;
using Raditap.BusinessLogic.ApiDataObjects;
using Raditap.BusinessLogic.ApiDataObjects.Users.Login;
using Raditap.BusinessLogic.Interfaces.Managers;
using Raditap.DatabaseAccess.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Raditap.BusinessLogic.Handlers.Authentication
{
    public class LoginHandler : IRequestHandler<LoginRequest, LoginResponse>
    {
        private readonly ILogger<LoginHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly ILoginManager _loginManager;

        public LoginHandler(ILogger<LoginHandler> logger,
                                   IUserRepository userRepository,
                                   ILoginManager loginManager)
        {
            _logger = logger;
            _userRepository = userRepository;
            _loginManager = loginManager;
        }

        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmail(request.Email);

            if(user == null)
            {
                _logger.LogWarning($"user not found for email: {request.Email}");
                return new LoginResponse(Result.UserNotFound);
            }

            var result = await _loginManager.Handle(request, user);

            if (!result.result.IsSuccess())
                return new LoginResponse(result.result);

            return new LoginResponse(result.result)
            {
                UserData = new LoginUserDto()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Token = _loginManager.GenerateToken(user)
                }
            };
        }
    }
}
