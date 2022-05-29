using System;
using System.ComponentModel.DataAnnotations;
using Raditap.BusinessLogic.ValidationAttributes.Results;
using Raditap.Utilities.Extensions;

namespace Raditap.BusinessLogic.ValidationAttributes.PropertyAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Convert.ToString(value))) return ValidationResult.Success;

            var input = value as string;

            var isValid = new EmailAddressAttribute().IsValid(input);
            if (!isValid) return new InvalidEmailFormatValidationResult(validationContext?.ObjectType.GetJsonName(validationContext.MemberName));

            return ValidationResult.Success;
        }
    }
}