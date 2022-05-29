using Raditap.DatabaseAccess.Entities;

namespace Raditap.DatabaseAccess.Specifications.Jobs
{
    public sealed class JobManualNumberFilterSpecification : BaseSpecification<Job>
    {
        public JobManualNumberFilterSpecification(string manualNumber) : base(x => x.ManualNumber == manualNumber)
        {
        }
    }
}
