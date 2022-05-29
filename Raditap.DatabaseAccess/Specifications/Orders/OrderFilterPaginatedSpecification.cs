using Raditap.DatabaseAccess.Entities;

namespace Raditap.DatabaseAccess.Specifications.Orders
{
    public sealed class OrderFilterPaginatedSpecification : BaseSpecification<Order>
    {
        public OrderFilterPaginatedSpecification(int? skip, int? take, string manualNumber, int statusId, string customerName, string customerMobileNumber, string description)
            : base(x => (string.IsNullOrWhiteSpace(manualNumber) || x.ManualNumber == manualNumber) &&
                        (statusId == 0|| x.OrderStatusId == statusId) &&
                        (string.IsNullOrWhiteSpace(customerName) || x.CustomerName == customerName) &&
                        (string.IsNullOrWhiteSpace(customerMobileNumber) || x.CustomerMobileNumber == customerMobileNumber) &&
                        (string.IsNullOrWhiteSpace(description) || x.Description.Contains(description)))
        {
            if (skip.HasValue && take.HasValue) ApplyPaging(skip.GetValueOrDefault(), take.GetValueOrDefault());
            AddInclude(x => x.OrderStatus);
            ApplyOrderByDescending(x => x.Id);
        }
    }
}
