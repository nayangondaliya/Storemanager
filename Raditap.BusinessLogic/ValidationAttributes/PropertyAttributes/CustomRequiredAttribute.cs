using System;
using System.ComponentModel.DataAnnotations;
using Raditap.BusinessLogic.ValidationAttributes.Results;
using Raditap.Utilities.Extensions;

namespace Raditap.BusinessLogic.ValidationAttributes.PropertyAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomRequiredAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var errorResponse = new FieldIsRequiredValidationResult(validationContext?.ObjectType.GetJsonName(validationContext.MemberName));
            if (value is byte[] b) return b.Length <= 0 ? errorResponse : ValidationResult.Success;

            if (value == null || string.IsNullOrWhiteSpace(Convert.ToString(value))) return errorResponse;

            return ValidationResult.Success;
        }
    }
}