namespace Raditap.DatabaseAccess.Specifications.Orders
{
    public sealed class OrderManualNumberSpecification : BaseSpecification<Entities.Order>
    {
        public OrderManualNumberSpecification() : base(x => x.ManualNumber == x.ManualNumber)
        {
            ApplyPaging(0, 1);
            ApplyOrderByDescending(x => x.Id);
        }
    }
}
