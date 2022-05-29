using System;
using System.ComponentModel.DataAnnotations;
using Raditap.BusinessLogic.ValidationAttributes.Results;
using Raditap.Utilities.Extensions;

namespace Raditap.BusinessLogic.ValidationAttributes.PropertyAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomMaxLengthAttribute : ValidationAttribute
    {
        private readonly int _maxLength;

        public CustomMaxLengthAttribute(int maxLength)
        {
            _maxLength = maxLength;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            var input = value as string;
            if (input?.Length > _maxLength)
                return new InvalidMaxLengthValidationResult(validationContext?.ObjectType.GetJsonName(validationContext.MemberName), _maxLength);

            return ValidationResult.Success;
        }
    }
}