using System;
using System.IO;
using System.Security.Cryptography;

namespace Raditap.Utilities.Cryptography
{
    public static class AesCryptography
    {
        public static string Encrypt(string plainText, string key, string iv)
        {
            if (string.IsNullOrWhiteSpace(plainText)) return string.Empty;
            return Encrypt(plainText, Convert.FromBase64String(key), Convert.FromBase64String(iv));
        }

        public static string Decrypt(string encryptedText, string key, string iv)
        {
            if (string.IsNullOrWhiteSpace(encryptedText)) return string.Empty;
            return Decrypt(encryptedText, Convert.FromBase64String(key), Convert.FromBase64String(iv));
        }

        private static string Encrypt(string plainText, byte[] key, byte[] iv)
        {
            using (var rijn = new RijndaelManaged { Mode = CipherMode.CBC, Padding = PaddingMode.PKCS7 })
            {
                var encryptor = rijn.CreateEncryptor(key, iv);

                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }

                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }

        private static string Decrypt(string cipherText, byte[] key, byte[] iv)
        {
            using (var rijn = new RijndaelManaged { Mode = CipherMode.CBC, Padding = PaddingMode.PKCS7 })
            {
                var decryptor = rijn.CreateDecryptor(key, iv);

                using (var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
