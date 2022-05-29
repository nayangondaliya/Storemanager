using System;
using System.ComponentModel.DataAnnotations;
using Raditap.BusinessLogic.ValidationAttributes.Results;
using Raditap.Utilities.Extensions;

namespace Raditap.BusinessLogic.ValidationAttributes.PropertyAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class BetweenAttribute : ValidationAttribute
    {
        private readonly int _min;
        private readonly int _max;

        public BetweenAttribute(int min, int max)
        {
            _min = min;
            _max = max;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var errorResponse = new BetweenErrorValidationResult(validationContext?.ObjectType.GetJsonName(validationContext.MemberName), _min, _max);
            if (value == null || !int.TryParse(Convert.ToString(value), out var input)) return errorResponse;

            var isValid = input >= _min && input <= _max;

            return isValid ? ValidationResult.Success : errorResponse;
        }
    }
}