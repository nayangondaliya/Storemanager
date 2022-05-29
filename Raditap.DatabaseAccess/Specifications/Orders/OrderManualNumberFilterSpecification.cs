namespace Raditap.DatabaseAccess.Specifications.Orders
{
    public sealed class OrderManualNumberFilterSpecification : BaseSpecification<Entities.Order>
    {
        public OrderManualNumberFilterSpecification(string manualNumber) : base(x => x.ManualNumber == manualNumber)
        {
        }
    }
}
