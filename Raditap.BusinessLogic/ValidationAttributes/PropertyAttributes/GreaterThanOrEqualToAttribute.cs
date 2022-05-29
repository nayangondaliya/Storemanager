using System;
using System.ComponentModel.DataAnnotations;
using Raditap.BusinessLogic.ValidationAttributes.Results;
using Raditap.Utilities.Extensions;

namespace Raditap.BusinessLogic.ValidationAttributes.PropertyAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class GreaterThanOrEqualToAttribute : ValidationAttribute
    {
        private readonly int _comparisonValue;

        public GreaterThanOrEqualToAttribute(int comparisonValue)
        {
            _comparisonValue = comparisonValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var errorResponse = new GreaterThanOrEqualValidationResult(validationContext?.ObjectType.GetJsonName(validationContext.MemberName), _comparisonValue);
            if (value == null || !int.TryParse(Convert.ToString(value), out var input)) return errorResponse;

            var isValid = input >= _comparisonValue;

            return isValid ? ValidationResult.Success : errorResponse;
        }
    }
}