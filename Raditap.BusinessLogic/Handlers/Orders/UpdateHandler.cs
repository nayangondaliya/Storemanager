using MediatR;
using Microsoft.Extensions.Logging;
using Raditap.BusinessLogic.ApiDataObjects;
using Raditap.BusinessLogic.ApiDataObjects.Orders;
using Raditap.BusinessLogic.Contexts;
using Raditap.DatabaseAccess.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Raditap.BusinessLogic.Handlers.Orders
{
    public class UpdateHandler : IRequestHandler<EditOrderRequest, EditOrderResponse>
    {
        private readonly ILogger<UpdateHandler> _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly UserContext _userContext;
        
        public UpdateHandler(ILogger<UpdateHandler> logger, IOrderRepository orderRepository, UserContext userContext)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _userContext = userContext;
        }

        public async Task<EditOrderResponse> Handle(EditOrderRequest request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);

            if (order == null)
            {
                _logger.LogWarning($"Order is not found with Id : {request.OrderId}");
                return new EditOrderResponse(Result.OrderNotFound);
            }

            if(order.ManualNumber != request.ManualNumber)
            {
                var manualNumberOrder = await _orderRepository.GetByManualNumber(request.ManualNumber);

                if(manualNumberOrder != null)
                {
                    _logger.LogWarning($"Order is already exists with manual number: {request.ManualNumber}");
                    return new EditOrderResponse(Result.OrderAlreadyExists);
                }
            }

            order.ManualNumber = request.ManualNumber;
            order.OrderStatusId = request.StatusId;
            order.SystemUserId = _userContext.CustomerData.Id;
            order.CustomerMobileNumber = request.CustomerMobileNumber;
            order.CustomerName = request.CustomerName;
            order.Description = request.Description;
            order.UpdatedAt = DateTime.Now;

            await _orderRepository.UpdateAsync(order);

            return new EditOrderResponse(Result.Success);
        }
    }
}
