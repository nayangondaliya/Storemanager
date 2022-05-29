using MediatR;
using Microsoft.Extensions.Logging;
using Raditap.BusinessLogic.ApiDataObjects;
using Raditap.BusinessLogic.ApiDataObjects.Orders;
using Raditap.DatabaseAccess.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Raditap.BusinessLogic.Handlers.Orders
{
    public class RemoveHandler : IRequestHandler<RemoveOrderRequest, RemoveOrderResponse>
    {
        private readonly ILogger<RemoveHandler> _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly IJobRepository _jobRepository;

        public RemoveHandler(ILogger<RemoveHandler> logger, IOrderRepository orderRepository, IJobRepository jobRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _jobRepository = jobRepository;
        }

        public async Task<RemoveOrderResponse> Handle(RemoveOrderRequest request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);

            if (order == null)
            {
                _logger.LogWarning($"Order is not found with Id : {request.OrderId}");
                return new RemoveOrderResponse(Result.OrderNotFound);
            }

            await _orderRepository.DeleteAsync(order);
            return new RemoveOrderResponse(Result.Success);
        }
    }
}
