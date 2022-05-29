using MediatR;
using Microsoft.Extensions.Logging;
using Raditap.BusinessLogic.ApiDataObjects;
using Raditap.BusinessLogic.ApiDataObjects.Users;
using Raditap.BusinessLogic.Contexts;
using Raditap.DatabaseAccess.Entities;
using Raditap.DatabaseAccess.Interfaces.Repositories;
using Raditap.DataObjects.AppSettings;
using Raditap.Utilities.Cryptography;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Raditap.BusinessLogic.Handlers.Users
{
    public class AddHandler : IRequestHandler<AddUserRequest, AddUserResponse>
    {
        private readonly ILogger<AddHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly DatabaseSettings _databaseSettings;
        private readonly UserContext _userContext;

        public AddHandler(ILogger<AddHandler> logger,
                           DatabaseSettings databaseSettings,
                           IUserRepository userRepository,
                           UserContext userContext)
        {
            _logger = logger;
            _userRepository = userRepository;
            _databaseSettings = databaseSettings;
            _userContext = userContext;
        }

        public async Task<AddUserResponse> Handle(AddUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmail(request.UserName);

            if (user != null)
            {
                _logger.LogWarning($"user is already exists with username : {request.UserName}");
                return new AddUserResponse(Result.UserAlreadyExists);
            }

            User newUser = new User();
            
            newUser.FirstName = request.FirstName;
            newUser.LastName = request.LastName;
            newUser.Email = request.UserName;
            newUser.Passcode = AesCryptography.Encrypt(request.Password, _databaseSettings.AesKey, _databaseSettings.AesIv);
            newUser.MobileNumber = string.IsNullOrEmpty(request.MobileNumber) ? 0 : Convert.ToInt64(request.MobileNumber);
            newUser.RegisterDate = DateTime.Now;
            newUser.SystemUserId = _userContext.CustomerData.Id;

            await _userRepository.AddAsync(newUser);

            return new AddUserResponse(Result.Success);
        }
    }
}
