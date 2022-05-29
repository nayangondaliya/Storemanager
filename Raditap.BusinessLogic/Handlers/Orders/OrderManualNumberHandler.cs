using MediatR;
using Microsoft.Extensions.Logging;
using Raditap.BusinessLogic.ApiDataObjects;
using Raditap.BusinessLogic.ApiDataObjects.Orders;
using Raditap.DatabaseAccess.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Raditap.BusinessLogic.Handlers.Orders
{
    public class OrderManualNumberHandler : IRequestHandler<LastOrderNumberRequest, LastOrderNumberResponse>
    {
        private readonly ILogger<OrderManualNumberHandler> _logger;
        private readonly IOrderRepository _orderRepository;

        public OrderManualNumberHandler(ILogger<OrderManualNumberHandler> logger, IOrderRepository orderRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }

        public async Task<LastOrderNumberResponse> Handle(LastOrderNumberRequest request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetLastOrder();
            var manualNumber = order?.ManualNumber == null ? 1 : Convert.ToInt64(order?.ManualNumber) + 1;

            var response = new LastOrderNumberResponse(Result.Success)
            {
                ManualNumber = Convert.ToString(manualNumber)
            };

            _logger.LogWarning($"Last Order Manual Number is : {response.ManualNumber}");
            return response;
        }
    }
}
