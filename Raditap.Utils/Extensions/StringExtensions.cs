using System;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;

namespace Raditap.Utilities.Extensions
{
    public static class StringExtensions
    {
        public static DateTime? ParseDateTime(this string dateTime, string format)
        {
            if (string.IsNullOrWhiteSpace(dateTime)) return null;

            return DateTime.TryParseExact(dateTime, format, CultureInfo.CurrentCulture, DateTimeStyles.None, out var result) ? (DateTime?)result : null;
        }

        public static DateTime? ParseDateTimeToUniversal(this string dateTime, string format)
        {
            if (string.IsNullOrWhiteSpace(dateTime)) return null;

            return DateTime.TryParseExact(dateTime, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result)
                           ? (DateTime?)result.ToUniversalTime()
                           : null;
        }

        public static decimal? ParseDecimal(this string val)
        {
            if (string.IsNullOrWhiteSpace(val)) return null;

            return decimal.TryParse(val, out var result) ? (decimal?)result : null;
        }

        public static bool? ParseBool(this string val)
        {
            if (string.IsNullOrWhiteSpace(val)) return null;

            return bool.TryParse(val, out var result) ? (bool?)result : null;
        }

        public static string ToBase64(this string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string TryGetResponseCode(this string response)
        {
            try
            {
                var baseResponse = JsonConvert.DeserializeObject<dynamic>(response);
                var responseCode = Convert.ToString(baseResponse.responseCode);
                return responseCode;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string TryGetResponseDescription(this string response)
        {
            try
            {
                var baseResponse = JsonConvert.DeserializeObject<dynamic>(response);
                var responseCode = Convert.ToString(baseResponse.responseDescription);
                return responseCode;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static bool IsValidDeviceToken(this string deviceToken)
        {
            if (string.IsNullOrWhiteSpace(deviceToken)) return false;

            return deviceToken.Equals("N/A", StringComparison.InvariantCultureIgnoreCase);
        }

        public static string GetLanguageOrDefault(this string languageCode)
        {
            if (string.IsNullOrWhiteSpace(languageCode)) return "EN";

            return languageCode;
        }

        public static string AppendUrlIfNotEmpty(this string url, string cloudFrontUrl)
        {
            if (string.IsNullOrWhiteSpace(url)) return string.Empty;

            return cloudFrontUrl + url;
        }
    }
}