namespace Raditap.DatabaseAccess.Specifications.Users
{
    public sealed class UserIdFilterSpecification : BaseSpecification<Entities.User>
    {
        public UserIdFilterSpecification(long id) : base(x => x.Id == id)
        {
            AddInclude(x => x.SystemUser);
        }
    }
}
