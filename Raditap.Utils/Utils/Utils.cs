using System;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Raditap.Utilities
{
    public static class Utils
    {
        public static bool IsValidJson(string strInput)
        {
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException)
                {
                    //Exception in parsing json
                    return false;
                }
                catch (Exception) //some other exception
                {
                    return false;
                }
            }

            return false;
        }

        public static string ReplaceModelInTemplate<T>(string template, T model, ILogger logger)
        {
            try
            {
                if (model != null)
                {
                    foreach (var prop in model.GetType().GetProperties())
                    {
                        var name = prop.Name;
                        var val = Convert.ToString(prop.GetValue(model, null));

                        template = template.Replace($"[{name}]", val);
                    }
                }

                return template;
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Error while replacing model in template: {e.Message}");
                return null;
            }
        }

        public static string RandomNumberString(int length)
        {
            if (length <= 0 || length >= 20) throw new ArgumentException("Length must be between 1 and 19");

            using (var random = RandomNumberGenerator.Create())
            {
                var data = new byte[8];
                random.GetBytes(data);
                return (BitConverter.ToUInt64(data) % Convert.ToUInt64(Math.Pow(10, length))).ToString().PadLeft(length, '0');
            }
        }

        public static decimal CalculateFee(decimal amount, decimal feeAmount, decimal feePercentage)
        {
            var feePercent = amount * (feePercentage / 100);

            return feeAmount + feePercent;
        }

        public static string HmacSha256(string text, string key)
        {
            var textBytes = Encoding.UTF8.GetBytes(text);
            var keyBytes = Encoding.UTF8.GetBytes(key);

            using (var hash = new HMACSHA256(keyBytes))
            {
                var hashBytes = hash.ComputeHash(textBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToUpper();
            }
        }

        public static bool IsJsonEquivalent(string json1, string json2)
        {
            try
            {
                if (json1 == null) return true;
                if (json2 == null) return true;
                if (json1.Equals(json2)) return true;

                var j1 = JObject.Parse(json1);
                var j2 = JObject.Parse(json2);

                return JToken.DeepEquals(j1, j2);
            }
            catch (Exception)
            {
                return true;
            }
        }

        public static string Pkcs7EncryptWithBase64(string text, string publicKeyBase64)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            var contentInfo = new ContentInfo(bytes);
            var envelopedCms = new EnvelopedCms(contentInfo);
            var publicCert = new X509Certificate2(Encoding.UTF8.GetBytes(publicKeyBase64));
            var recipient = new CmsRecipient(SubjectIdentifierType.IssuerAndSerialNumber, publicCert);
            envelopedCms.Encrypt(recipient);

            return Convert.ToBase64String(envelopedCms.Encode());
        }

        public static string Pkcs7Decrypt(string encryptedString, byte[] privateKey, string privateKeyPwd)
        {
            var envelopedCms = new EnvelopedCms();
            var encodedMessage = Convert.FromBase64String(encryptedString);
            envelopedCms.Decode(encodedMessage);

            var privateCert = new X509Certificate2(privateKey, privateKeyPwd, X509KeyStorageFlags.MachineKeySet);
            envelopedCms.Decrypt(envelopedCms.RecipientInfos[0], new X509Certificate2Collection(privateCert));

            var result = Encoding.UTF8.GetString(envelopedCms.ContentInfo.Content);
            return result;
        }
    }
}