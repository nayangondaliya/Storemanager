namespace Raditap.DatabaseAccess.Specifications.Users
{
    public sealed class UserEmailFilterSpecification : BaseSpecification<Entities.User>
    {
        public UserEmailFilterSpecification(string email) : base(x => x.Email == email)
        {
        }
    }
}
