using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Raditap.BusinessLogic.ValidationAttributes.Results;
using Raditap.Utilities.Extensions;

namespace Raditap.BusinessLogic.ValidationAttributes.PropertyAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DateTimeAttribute : ValidationAttribute
    {
        private readonly string _format;

        public DateTimeAttribute(string format)
        {
            _format = format;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var data = Convert.ToString(value);
            if (string.IsNullOrEmpty(data) || string.IsNullOrWhiteSpace(data)) return ValidationResult.Success;

            var input = value as string;
            var isValid = DateTime.TryParseExact(input, _format, CultureInfo.CurrentCulture, DateTimeStyles.None, out _);
            return isValid ? ValidationResult.Success : new InvalidDateTimeValidationResult(validationContext?.ObjectType.GetJsonName(validationContext.MemberName));
        }
    }
}