using System.ComponentModel.DataAnnotations;
using Raditap.BusinessLogic.ApiDataObjects.Common;

namespace Raditap.BusinessLogic.ValidationAttributes.Results
{
    public class InvalidFieldValueValidationResult : ValidationResult
    {
        public InvalidFieldValueValidationResult(string field) : base(new InvalidFieldValueResponse(field).ToString()) { }
    }
}