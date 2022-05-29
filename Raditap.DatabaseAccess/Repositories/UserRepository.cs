using Raditap.DatabaseAccess.Entities;
using Raditap.DatabaseAccess.Interfaces.Repositories;
using Raditap.DatabaseAccess.Specifications.Users;
using Raditap.DataObjects.AppSettings;
using Raditap.Utilities.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raditap.DatabaseAccess.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(RaditapContext writeContext, ReadRaditapContext readContext, DatabaseSettings databaseSettings, ProcessingTimeHelper timeHelper) :
                base(writeContext, readContext, databaseSettings, timeHelper)
        { }

        public async Task<int> Count(string firstName, string lastName, string userName, string mobileNumber)
        {
            var spec = new UserFilterPaginatedSpecification(null, null, firstName, lastName, userName, mobileNumber);
            var total = await CountAsync(spec);
            return total;
        }

        public async Task<User> GetByEmail(string email)
        {
            var spec = new UserEmailFilterSpecification(email);
            var user = (await ListAsync(spec)).FirstOrDefault();
            return user;
        }

        public async Task<User> GetUserById(long userId)
        {
            var spec = new UserIdFilterSpecification(userId);
            var user = (await ListAsync(spec)).FirstOrDefault();
            return user;
        }

        public async Task<List<User>> GetUsersById(long userId)
        {
            var spec = new UsersByIdFilterSpecification(userId);
            var users = await ListAsync(spec);
            return users?.ToList();
        }

        public async Task<IReadOnlyList<User>> List(int? skip, int? take, string firstName, string lastName, string userName, string mobileNumber)
        {
            var spec = new UserFilterPaginatedSpecification(skip, take, firstName, lastName, userName, mobileNumber);
            var users = await ListAsync(spec);
            return users;
        }
    }
}
