using System.ComponentModel.DataAnnotations;
using Raditap.BusinessLogic.ApiDataObjects.Common;

namespace Raditap.BusinessLogic.ValidationAttributes.Results
{
    public class InvalidDateTimeValidationResult : ValidationResult
    {
        public InvalidDateTimeValidationResult(string field) : base(new InvalidDateTimeResponse(field).ToString()) { }
    }
}