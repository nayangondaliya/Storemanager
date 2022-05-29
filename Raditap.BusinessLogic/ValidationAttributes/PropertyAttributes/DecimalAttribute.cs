using System;
using System.ComponentModel.DataAnnotations;
using Raditap.BusinessLogic.ValidationAttributes.Results;
using Raditap.Utilities.Extensions;

namespace Raditap.BusinessLogic.ValidationAttributes.PropertyAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DecimalAttribute : ValidationAttribute
    {
        private readonly int _precision;
        private readonly int _scale;
        //private readonly bool _ignoreTrailingZeros = false;

        public DecimalAttribute(int precision, int scale)
        {
            _precision = precision;
            _scale = scale;

            if (_scale < 0) throw new ArgumentOutOfRangeException(nameof(scale), $"Scale must be a positive integer. [value:{_scale}].");
            if (_precision < 0) throw new ArgumentOutOfRangeException(nameof(precision), $"Precision must be a positive integer. [value:{_precision}].");
            if (_precision < _scale)
                throw new ArgumentOutOfRangeException(nameof(scale), $"Scale must be less than precision. [scale:{_scale}, precision:{_precision}].");
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            var input = Convert.ToString(value);

            if (!decimal.TryParse(input, out var decimalValue))
                return new InvalidFieldValueValidationResult(validationContext?.ObjectType.GetJsonName(validationContext.MemberName));

            var scale = GetScale(decimalValue);
            var precision = GetPrecision(decimalValue);
            if (scale > _scale || precision > _precision)
                return new InvalidDecimalValidationResult(validationContext?.ObjectType.GetJsonName(validationContext.MemberName), _precision, _scale);

            return ValidationResult.Success;
        }

        private static UInt32[] GetBits(decimal Decimal)
        {
            // We want the integer parts as uint
            // C# doesn't permit int[] to uint[] conversion, but .NET does. This is somewhat evil...
            return (uint[]) (object) decimal.GetBits(Decimal);
        }

        private static decimal GetMantissa(decimal Decimal)
        {
            var bits = GetBits(Decimal);
            return (bits[2] * 4294967296m * 4294967296m) + (bits[1] * 4294967296m) + bits[0];
        }

        private static uint GetUnsignedScale(decimal Decimal)
        {
            var bits = GetBits(Decimal);
            uint scale = (bits[3] >> 16) & 31;
            return scale;
        }

        private int GetScale(decimal Decimal)
        {
            uint scale = GetUnsignedScale(Decimal);
            //if (_ignoreTrailingZeros)
            //{
            //    return (int) (scale - NumTrailingZeros(Decimal));
            //}

            return (int) scale;
        }

        private static uint NumTrailingZeros(decimal Decimal)
        {
            uint trailingZeros = 0;
            uint scale = GetUnsignedScale(Decimal);
            for (decimal tmp = GetMantissa(Decimal); tmp % 10m == 0 && trailingZeros < scale; tmp /= 10)
            {
                trailingZeros++;
            }

            return trailingZeros;
        }

        private int GetPrecision(decimal Decimal)
        {
            // Precision: number of times we can divide by 10 before we get to 0        
            uint precision = 0;
            if (Decimal != 0m)
            {
                for (decimal tmp = GetMantissa(Decimal); tmp >= 1; tmp /= 10)
                {
                    precision++;
                }

                //if (_ignoreTrailingZeros)
                //{
                //    return (int) (precision - NumTrailingZeros(Decimal));
                //}
            }
            else
            {
                // Handle zero differently. It's odd.
                precision = (uint) GetScale(Decimal) + 1;
            }

            return (int) precision;
        }
    }
}