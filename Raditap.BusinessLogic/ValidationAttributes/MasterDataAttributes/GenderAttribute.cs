using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Raditap.BusinessLogic.ValidationAttributes.Results;
using Raditap.DataObjects.Raditap;
using Raditap.Utilities.Extensions;

namespace Raditap.BusinessLogic.ValidationAttributes.MasterDataAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class GenderAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var input = Convert.ToString(value);
            if (string.IsNullOrWhiteSpace(input)) return ValidationResult.Success;

            var genderTypes = RaditapGenderTypes.List;

            var isValid = genderTypes.FirstOrDefault(x => x.Value == input) != null;
            return isValid
                           ? ValidationResult.Success
                           : new RequiredOneOfValuesValidationResult(validationContext?.ObjectType.GetJsonName(validationContext.MemberName),
                                                                     string.Join(",", genderTypes.Select(i => i.Value).ToArray()));
        }
    }
}