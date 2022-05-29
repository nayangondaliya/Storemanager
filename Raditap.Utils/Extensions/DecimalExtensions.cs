using System;

namespace Raditap.Utilities.Extensions
{
    public static class DecimalExtensions
    {
        public static string Format2DigitsWithoutComma(this decimal val)
        {
            return val.ToString("N2").Replace(",", string.Empty);
        }

        public static string Format4DigitsWithoutComma(this decimal val)
        {
            return val.ToString("N4").Replace(",", string.Empty);
        }

        public static decimal RoundUp(this decimal val, int decimals = 4)
        {
            return Math.Round(val, decimals, MidpointRounding.AwayFromZero);
        }

        public static string Format2Digits(this decimal val)
        {
            return val.ToString("N2");
        }
    }
}
