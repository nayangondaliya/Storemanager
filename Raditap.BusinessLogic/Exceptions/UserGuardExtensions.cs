using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;
using Raditap.BusinessLogic.ApiDataObjects;
using Raditap.DatabaseAccess.Entities;

namespace Raditap.BusinessLogic.Exceptions
{
    public static class UserGuardExtensions
    {
        public static void NullUserByEmail(this IGuardClause guardClause, ILogger logger, User user, string email)
        {
            if (user == null)
            {
                logger.LogInformation($"Not found user email: {email}");
                throw new GeneralNotFoundException(Result.UserNotFound);
            }
        }
    }
}
