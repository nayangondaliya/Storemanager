using System;
using System.ComponentModel.DataAnnotations;
using Raditap.BusinessLogic.ValidationAttributes.Results;

namespace Raditap.BusinessLogic.ValidationAttributes.MasterDataAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AmountMoreThanZeroAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            var input = Convert.ToString(value);

            if (!decimal.TryParse(input, out var decimalValue)) return new InvalidAmountValidationResult();
            if (decimalValue <= 0) return new InvalidAmountValidationResult();

            return ValidationResult.Success;
        }
    }
}