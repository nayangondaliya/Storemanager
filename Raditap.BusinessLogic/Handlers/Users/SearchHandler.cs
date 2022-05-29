using MediatR;
using Microsoft.Extensions.Logging;
using Raditap.BusinessLogic.ApiDataObjects;
using Raditap.BusinessLogic.ApiDataObjects.Users;
using Raditap.DatabaseAccess.Interfaces.Repositories;
using Raditap.DataObjects.AppSettings;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Raditap.BusinessLogic.Handlers.Users
{
    public class SearchHandler : IRequestHandler<SearchUserRequest, SearchUserResponse>
    {
        private readonly ILogger<SearchHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly FormatSettings _formatSettings;

        public SearchHandler(ILogger<SearchHandler> logger,
                                   IUserRepository userRepository, FormatSettings formatSettings)
        {
            _logger = logger;
            _userRepository = userRepository;
            _formatSettings = formatSettings;
        }

        public async Task<SearchUserResponse> Handle(SearchUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.UserId);

            if (user == null)
            {
                _logger.LogWarning($"user is not found with Id : {request.UserId}");
                return new SearchUserResponse(Result.UserNotFound);
            }

            return new SearchUserResponse(Result.Success)
            {
                User = new UserDto()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserId = user.Id,
                    MobileNumber = Convert.ToString(user.MobileNumber),
                    UserName = user.Email,
                    CreatedAt = user.RegisterDate.ToString(_formatSettings.DateTimeFormat),
                    UpdatedAt = user.LastUpdated?.ToString(_formatSettings.DateTimeFormat),
                    SystemUser = user.SystemUser?.Fullname
                }
            };
        }
    }
}
