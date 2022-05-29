using MediatR;
using Microsoft.Extensions.Logging;
using Raditap.BusinessLogic.ApiDataObjects;
using Raditap.BusinessLogic.ApiDataObjects.Orders;
using Raditap.DatabaseAccess.Interfaces.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Raditap.BusinessLogic.Handlers.Orders
{
    public class ListHandler : IRequestHandler<ListOrderRequest, ListOrderResponse>
    {
        private readonly ILogger<ListHandler> _logger;
        private readonly IOrderRepository _orderRepository;

        public ListHandler(ILogger<ListHandler> logger,
                                   IOrderRepository orderRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }

        public async Task<ListOrderResponse> Handle(ListOrderRequest request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.List(request.GetSkip(), request.PageSize, request.ManualNumber, request.StatusId, request.CustomerName, request.CustomerMobileNumber, request.Description);
            var totalOrders = await _orderRepository.Count(request.ManualNumber, request.StatusId, request.CustomerName, request.CustomerMobileNumber, request.Description);

            var result = new ListOrderResponse(Result.Success)
            {
                PageInfo = new PageInfo(totalOrders, request.PageSize),
                Orders = orders?.Select(x => new ListOrderDto()
                {
                    ManualNumber = x.ManualNumber,
                    OrderId = x.Id,
                    Status = x.OrderStatus.Status,
                    CustomerMobileNumber = x.CustomerMobileNumber,
                    CustomerName = x.CustomerName
                }).ToList()
            };

            return result;
        }
    }
}
