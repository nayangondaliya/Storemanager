using System.ComponentModel.DataAnnotations;
using Raditap.BusinessLogic.ApiDataObjects.Common;

namespace Raditap.BusinessLogic.ValidationAttributes.Results
{
    public class InvalidEmailFormatValidationResult : ValidationResult
    {
        public InvalidEmailFormatValidationResult(string field) : base(new InvalidEmailFormatResponse(field).ToString()) { }
    }
}