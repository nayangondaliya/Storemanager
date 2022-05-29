namespace Raditap.DatabaseAccess.Specifications.Users
{
    public sealed class UsersByIdFilterSpecification : BaseSpecification<Entities.User>
    {
        public UsersByIdFilterSpecification(long userId) : base(x => x.SystemUserId == userId)
        {
        }
    }
}
