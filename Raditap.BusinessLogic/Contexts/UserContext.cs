using Newtonsoft.Json;
using Raditap.DataObjects.AppSettings;
using Raditap.Utilities.Cryptography;
using System.Linq;
using System.Security.Claims;

namespace Raditap.BusinessLogic.Contexts
{
    public class UserContext
    {
        public virtual LoginUserData CustomerData { get; }

        public UserContext(ClaimsIdentity claim, AppSettings appSettings)
        {
            if (!claim.IsAuthenticated) return;

            var userData = claim.Claims.FirstOrDefault(x => x.Type == ClaimTypes.UserData)?.Value;

            var decryptedData = AesCryptography.Decrypt(userData, appSettings.AesKey, appSettings.AesIv);

            CustomerData = JsonConvert.DeserializeObject<LoginUserData>(decryptedData);
        }
    }
}
