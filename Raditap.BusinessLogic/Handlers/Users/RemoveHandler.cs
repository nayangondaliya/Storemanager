using MediatR;
using Microsoft.Extensions.Logging;
using Raditap.BusinessLogic.ApiDataObjects;
using Raditap.BusinessLogic.ApiDataObjects.Users;
using Raditap.DatabaseAccess.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Raditap.BusinessLogic.Handlers.Users
{
    public class RemoveHandler : IRequestHandler<RemoveUserRequest, RemoveUserResponse>
    {
        private readonly ILogger<RemoveHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IOrderRepository _orderRepository;

        public RemoveHandler(ILogger<RemoveHandler> logger, IUserRepository userRepository, IJobRepository jobRepository
                            , IOrderRepository orderRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _jobRepository = jobRepository;
            _orderRepository = orderRepository;
        }

        public async Task<RemoveUserResponse> Handle(RemoveUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user == null)
            {
                _logger.LogWarning($"user is not found with Id : {request.UserId}");
                return new RemoveUserResponse(Result.UserNotFound);
            }

            var users = await _userRepository.GetUsersById(user.Id);
            if (users?.Count > 0)
            {
                foreach (var dUser in users)
                {
                    dUser.SystemUserId = null;
                    await _userRepository.UpdateAsync(dUser);
                }
            }

            var orders = await _orderRepository.GetOrdersByUserId(user.Id);
            if(orders?.Count > 0)
            {
                foreach(var order in orders)
                {
                    order.SystemUserId = null;
                    await _orderRepository.UpdateAsync(order);
                }
            }

            var jobs = await _jobRepository.GetJobsByUserId(user.Id);
            if (jobs?.Count > 0)
            {
                foreach (var job in jobs)
                {
                    job.SystemUserId = null;
                    await _jobRepository.UpdateAsync(job);
                }
            }

            await _userRepository.DeleteAsync(user);
            return new RemoveUserResponse(Result.Success);
        }
    }
}
