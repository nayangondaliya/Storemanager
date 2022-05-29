namespace Raditap.DatabaseAccess.Specifications.Orders
{
    public sealed class OrdersByUserIdFilterSpecification : BaseSpecification<Entities.Order>
    {
        public OrdersByUserIdFilterSpecification(long userId) : base(x => x.SystemUserId == userId)
        {
        }
    }
}
