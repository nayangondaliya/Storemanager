using System.ComponentModel.DataAnnotations;
using Raditap.BusinessLogic.ApiDataObjects.Common;

namespace Raditap.BusinessLogic.ValidationAttributes.Results
{
    public class InvalidBooleanValidationResult : ValidationResult
    {
        public InvalidBooleanValidationResult(string field) : base(new InvalidBooleanResponse(field).ToString()) { }
    }
}