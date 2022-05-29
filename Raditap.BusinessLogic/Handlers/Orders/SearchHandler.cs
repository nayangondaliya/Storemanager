using MediatR;
using Microsoft.Extensions.Logging;
using Raditap.BusinessLogic.ApiDataObjects;
using Raditap.BusinessLogic.ApiDataObjects.Orders;
using Raditap.DatabaseAccess.Interfaces.Repositories;
using Raditap.DataObjects.AppSettings;
using System.Threading;
using System.Threading.Tasks;

namespace Raditap.BusinessLogic.Handlers.Orders
{
    public class SearchHandler : IRequestHandler<SearchOrderRequest, SearchOrderResponse>
    {
        private readonly ILogger<SearchHandler> _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly FormatSettings _formatSettings;

        public SearchHandler(ILogger<SearchHandler> logger, IOrderRepository orderRepository, FormatSettings formatSettings)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _formatSettings = formatSettings;
        }

        public async Task<SearchOrderResponse> Handle(SearchOrderRequest request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderById(request.OrderId);

            if (order == null)
            {
                _logger.LogWarning($"Order is not found with Id : {request.OrderId}");
                return new SearchOrderResponse(Result.OrderNotFound);
            }

            return new SearchOrderResponse(Result.Success)
            {
                Order = new OrderDto()
                {
                    CreatedAt = order.CreatedAt.ToString(_formatSettings.DateTimeFormat),
                    CustomerMobileNumber = order.CustomerMobileNumber,
                    CustomerName = order.CustomerName,
                    Description = order.Description,
                    ManualNumber = order.ManualNumber,
                    OrderId = order.Id,
                    StatusId = order.OrderStatusId,
                    UpdatedAt = order.UpdatedAt?.ToString(_formatSettings.DateTimeFormat),
                    SystemUser = order.SystemUser?.Fullname
                }
            };
        }
    }
}
