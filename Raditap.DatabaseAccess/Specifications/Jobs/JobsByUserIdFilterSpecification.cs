namespace Raditap.DatabaseAccess.Specifications.Jobs
{
    public sealed class JobsByUserIdFilterSpecification : BaseSpecification<Entities.Job>
    {
        public JobsByUserIdFilterSpecification(long userId) : base(x => x.SystemUserId == userId)
        {
        }
    }
}
