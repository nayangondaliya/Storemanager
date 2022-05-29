using Raditap.DatabaseAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Raditap.DatabaseAccess.Interfaces.Repositories
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<User> GetUserById(long userId);
        Task<User> GetByEmail(string email);
        Task<IReadOnlyList<User>> List(int? skip, int? take, string firstName, string lastName, string userName, string mobileNumber);
        Task<int> Count(string firstName, string lastName, string userName, string mobileNumber);
        Task<List<User>> GetUsersById(long userId);
    }
}
