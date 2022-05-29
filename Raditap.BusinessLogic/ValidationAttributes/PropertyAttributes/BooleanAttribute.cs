using System;
using System.ComponentModel.DataAnnotations;
using Raditap.BusinessLogic.ValidationAttributes.Results;
using Raditap.Utilities.Extensions;

namespace Raditap.BusinessLogic.ValidationAttributes.PropertyAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class BooleanAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            var input = Convert.ToString(value);
            var isValid = bool.TryParse(input, out _);
            return isValid ? ValidationResult.Success : new InvalidBooleanValidationResult(validationContext?.ObjectType.GetJsonName(validationContext.MemberName));
        }
    }
}