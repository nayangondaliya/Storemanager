using MediatR;
using Microsoft.Extensions.Logging;
using Raditap.BusinessLogic.ApiDataObjects;
using Raditap.BusinessLogic.ApiDataObjects.Orders;
using Raditap.BusinessLogic.Contexts;
using Raditap.DatabaseAccess.Entities;
using Raditap.DatabaseAccess.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Raditap.BusinessLogic.Handlers.Orders
{
    public class AddHandler : IRequestHandler<AddOrderRequest, AddOrderResponse>
    {
        private readonly ILogger<AddHandler> _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly UserContext _userContext;

        public AddHandler(ILogger<AddHandler> logger, IOrderRepository orderRepository, UserContext userContext)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _userContext = userContext;
        }

        public async Task<AddOrderResponse> Handle(AddOrderRequest request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByManualNumber(request.ManualNumber);

            if(order != null)
            {
                _logger.LogWarning($"Order is already exists with manual number: {request.ManualNumber}");
                return new AddOrderResponse(Result.OrderAlreadyExists);
            }

            Order newOrder = new Order();
            newOrder.CustomerName = request.CustomerName;
            newOrder.Description = request.Description;
            newOrder.CreatedAt = DateTime.Now;
            newOrder.ManualNumber = request.ManualNumber;
            newOrder.OrderStatusId = request.StatusId;
            newOrder.SystemUserId = _userContext.CustomerData.Id;
            newOrder.CustomerMobileNumber = request.CustomerMobileNumber;

            await _orderRepository.AddAsync(newOrder);

            return new AddOrderResponse(Result.Success);
        }
    }
}
