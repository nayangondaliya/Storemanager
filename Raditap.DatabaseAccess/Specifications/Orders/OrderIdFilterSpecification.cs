namespace Raditap.DatabaseAccess.Specifications.Orders
{
    public sealed class OrderIdFilterSpecification : BaseSpecification<Entities.Order>
    {
        public OrderIdFilterSpecification(long id) : base(x => x.Id == id)
        {
            AddInclude(x => x.SystemUser);
            AddInclude(x => x.OrderStatus);
        }
    }
}
