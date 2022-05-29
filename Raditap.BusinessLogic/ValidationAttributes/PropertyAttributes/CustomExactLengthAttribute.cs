using System;
using System.ComponentModel.DataAnnotations;
using Raditap.BusinessLogic.ValidationAttributes.Results;
using Raditap.Utilities.Extensions;

namespace Raditap.BusinessLogic.ValidationAttributes.PropertyAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomExactLengthAttribute : ValidationAttribute
    {
        private readonly int _length;

        public CustomExactLengthAttribute(int length)
        {
            _length = length;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            var input = value as string;
            if (string.IsNullOrWhiteSpace(input)) return ValidationResult.Success;

            if (input?.Length != _length) return new InvalidExactLengthValidationResult(validationContext?.ObjectType.GetJsonName(validationContext.MemberName), _length);

            return ValidationResult.Success;
        }
    }
}