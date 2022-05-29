using System.ComponentModel.DataAnnotations;
using Raditap.BusinessLogic.ApiDataObjects.Common;

namespace Raditap.BusinessLogic.ValidationAttributes.Results
{
    public class InvalidMinLengthValidationResult : ValidationResult
    {
        public InvalidMinLengthValidationResult(string field, int minLength) : base(new InvalidMinLengthResponse(field, minLength).ToString()) { }
    }
}