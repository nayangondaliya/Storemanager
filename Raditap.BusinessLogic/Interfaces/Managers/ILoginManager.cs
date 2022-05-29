using System.Threading.Tasks;
using Raditap.BusinessLogic.ApiDataObjects;
using Raditap.BusinessLogic.ApiDataObjects.Users.Login;
using Raditap.DatabaseAccess.Entities;

namespace Raditap.BusinessLogic.Interfaces.Managers
{
    public interface ILoginManager
    {
        Task<(Result result, string userData)> Handle(LoginRequestBase request, User user);
        string GenerateToken(User user);
    }
}
