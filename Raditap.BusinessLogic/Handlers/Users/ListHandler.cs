using MediatR;
using Microsoft.Extensions.Logging;
using Raditap.BusinessLogic.ApiDataObjects;
using Raditap.BusinessLogic.ApiDataObjects.Users;
using Raditap.DatabaseAccess.Interfaces.Repositories;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Raditap.BusinessLogic.Handlers.Users
{
    public class ListHandler : IRequestHandler<ListUserRequest, ListUserResponse>
    {
        private readonly ILogger<ListHandler> _logger;
        private readonly IUserRepository _userRepository;

        public ListHandler(ILogger<ListHandler> logger,
                                   IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<ListUserResponse> Handle(ListUserRequest request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.List(request.GetSkip(), request.PageSize, request.FirstName, request.LastName, request.UserName, request.MobileNumber);
            var totalUsers = await _userRepository.Count(request.FirstName, request.LastName, request.UserName, request.MobileNumber);

            var result = new ListUserResponse(Result.Success)
            {
                PageInfo = new PageInfo(totalUsers, request.PageSize),
                Users = users?.Select(x => new ListUserDto()
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    MobileNumber = Convert.ToString(x.MobileNumber),
                    UserId = x.Id,
                    UserName = x.Email
                }).ToList()
            };

            return result;
        }
    }
}
