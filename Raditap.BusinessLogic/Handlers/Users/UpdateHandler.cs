using MediatR;
using Microsoft.Extensions.Logging;
using Raditap.BusinessLogic.ApiDataObjects;
using Raditap.BusinessLogic.ApiDataObjects.Users;
using Raditap.DatabaseAccess.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Raditap.BusinessLogic.Handlers.Users
{
    public class UpdateHandler : IRequestHandler<EditUserRequest, EditUserResponse>
    {
        private readonly ILogger<UpdateHandler> _logger;
        private readonly IUserRepository _userRepository;

        public UpdateHandler(ILogger<UpdateHandler> logger,
                                   IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<EditUserResponse> Handle(EditUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user == null)
            {
                _logger.LogWarning($"user is not found with Id : {request.UserId}");
                return new EditUserResponse(Result.UserNotFound);
            }

            if (user.Email != request.UserName) 
            {
                var emailUser = await _userRepository.GetByEmail(request.UserName);

                if(emailUser != null)
                {
                    _logger.LogWarning($"user is already exists with username: {request.UserName}");
                    return new EditUserResponse(Result.UserAlreadyExists);
                }

                user.Email = request.UserName;
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.LastUpdated = DateTime.Now;
            user.MobileNumber = string.IsNullOrEmpty(request.MobileNumber) ? 0 : Convert.ToInt64(request.MobileNumber);

            await _userRepository.UpdateAsync(user);

            return new EditUserResponse(Result.Success);
        }
    }
}
