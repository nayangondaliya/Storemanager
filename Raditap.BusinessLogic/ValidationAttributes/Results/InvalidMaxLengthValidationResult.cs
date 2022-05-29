using System.ComponentModel.DataAnnotations;
using Raditap.BusinessLogic.ApiDataObjects.Common;

namespace Raditap.BusinessLogic.ValidationAttributes.Results
{
    public class InvalidMaxLengthValidationResult : ValidationResult
    {
        public InvalidMaxLengthValidationResult(string field, int maxLength) : base(new InvalidMaxLengthResponse(field, maxLength).ToString()) { }
    }
}