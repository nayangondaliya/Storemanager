using System.ComponentModel.DataAnnotations;
using Raditap.BusinessLogic.ApiDataObjects.Common;

namespace Raditap.BusinessLogic.ValidationAttributes.Results
{
    public class RequiredOneOfValuesValidationResult : ValidationResult
    {
        public RequiredOneOfValuesValidationResult(string field, string listOfValues) : base(new RequiredOneOfValuesResponse(field, listOfValues).ToString()) { }
    }
}