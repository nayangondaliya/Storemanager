using System;
using System.ComponentModel.DataAnnotations;
using Raditap.BusinessLogic.ValidationAttributes.Results;
using Raditap.Utilities.Extensions;

namespace Raditap.BusinessLogic.ValidationAttributes.PropertyAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomMinLengthAttribute : ValidationAttribute
    {
        private readonly int _minLength;

        public CustomMinLengthAttribute(int minLength)
        {
            _minLength = minLength;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            var input = value as string;
            if (string.IsNullOrWhiteSpace(input) || input.Length < _minLength)
                return new InvalidMinLengthValidationResult(validationContext?.ObjectType.GetJsonName(validationContext.MemberName), _minLength);

            return ValidationResult.Success;
        }
    }
}