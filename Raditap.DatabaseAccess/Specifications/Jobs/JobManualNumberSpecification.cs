using Raditap.DatabaseAccess.Entities;

namespace Raditap.DatabaseAccess.Specifications.Jobs
{
    public sealed class JobManualNumberSpecification : BaseSpecification<Job>
    {
        public JobManualNumberSpecification() : base(x => x.ManualNumber == x.ManualNumber)
        {
            ApplyPaging(0, 1);
            ApplyOrderByDescending(x => x.Id);
        }
    }
}
