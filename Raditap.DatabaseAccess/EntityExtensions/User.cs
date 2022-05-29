using Raditap.Utilities.Cryptography;
using System.ComponentModel.DataAnnotations.Schema;

namespace Raditap.DatabaseAccess.Entities
{
    public partial class User
    {
        public string DecryptLoginPasscode(string password, string key, string iv)
        {
            return AesCryptography.Decrypt(password, key, iv);
        }

        public string EncryptLoginPasscode(string data, string key, string iv)
        {
            return AesCryptography.Encrypt(data, key, iv);
        }

        [NotMapped]
        public string Fullname => $"{FirstName} {LastName}";
    }
}
