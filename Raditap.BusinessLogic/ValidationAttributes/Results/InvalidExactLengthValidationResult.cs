using System.ComponentModel.DataAnnotations;
using Raditap.BusinessLogic.ApiDataObjects.Common;

namespace Raditap.BusinessLogic.ValidationAttributes.Results
{
    public class InvalidExactLengthValidationResult : ValidationResult
    {
        public InvalidExactLengthValidationResult(string field, int length) : base(new InvalidExactLengthResponse(field, length).ToString()) { }
    }
}