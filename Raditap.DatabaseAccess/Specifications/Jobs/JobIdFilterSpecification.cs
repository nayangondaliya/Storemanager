using Raditap.DatabaseAccess.Entities;

namespace Raditap.DatabaseAccess.Specifications.Jobs
{
    public sealed class JobIdFilterSpecification : BaseSpecification<Job>
    {
        public JobIdFilterSpecification(long id) : base(x => x.Id == id)
        {
            AddInclude(x => x.JobStatus);
            AddInclude(x => x.SystemUser);
        }
    }
}
