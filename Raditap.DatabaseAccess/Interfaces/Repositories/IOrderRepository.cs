using Raditap.DatabaseAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Raditap.DatabaseAccess.Interfaces.Repositories
{
    public interface IOrderRepository : IAsyncRepository<Order>
    {
        Task<Order> GetOrderById(long orderId);
        Task<Order> GetByManualNumber(string manualNumber);
        Task<IReadOnlyList<Order>> List(int? skip, int? take, string manualNumber, int statusId, string customerName, string customerMobileNumber, string description);
        Task<int> Count(string manualNumber, int statusId, string customerName, string customerMobileNumber, string description);
        Task<Order> GetLastOrder();
        Task<List<Order>> GetOrdersByUserId(long userId);
    }
}
