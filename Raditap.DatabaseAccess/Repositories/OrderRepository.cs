using Raditap.DatabaseAccess.Entities;
using Raditap.DatabaseAccess.Interfaces.Repositories;
using Raditap.DatabaseAccess.Specifications.Orders;
using Raditap.DataObjects.AppSettings;
using Raditap.Utilities.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raditap.DatabaseAccess.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(RaditapContext writeContext, ReadRaditapContext readContext, DatabaseSettings databaseSettings, ProcessingTimeHelper timeHelper) :
                 base(writeContext, readContext, databaseSettings, timeHelper)
        { }

        public async Task<int> Count(string manualNumber, int statusId, string customerName, string customerMobileNumber, string description)
        {
            var spec = new OrderFilterPaginatedSpecification(null, null, manualNumber, statusId, customerName, customerMobileNumber, description);
            var total = await CountAsync(spec);
            return total;
        }

        public async Task<Order> GetByManualNumber(string manualNumber)
        {
            var spec = new OrderManualNumberFilterSpecification(manualNumber);
            var order = (await ListAsync(spec)).FirstOrDefault();
            return order;
        }

        public async Task<Order> GetLastOrder()
        {
            var spec = new OrderManualNumberSpecification();
            var order = (await ListAsync(spec)).FirstOrDefault();
            return order;
        }

        public async Task<Order> GetOrderById(long orderId)
        {
            var spec = new OrderIdFilterSpecification(orderId);
            var order = (await ListAsync(spec)).FirstOrDefault();
            return order;
        }

        public async Task<List<Order>> GetOrdersByUserId(long userId)
        {
            var spec = new OrdersByUserIdFilterSpecification(userId);
            var orders = await ListAsync(spec);
            return orders?.ToList();
        }

        public async Task<IReadOnlyList<Order>> List(int? skip, int? take, string manualNumber, int statusId, string customerName, string customerMobileNumber, string description)
        {
            var spec = new OrderFilterPaginatedSpecification(skip, take, manualNumber, statusId, customerName, customerMobileNumber, description);
            var orders = await ListAsync(spec);
            return orders;
        }
    }
}
