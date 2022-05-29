using System.ComponentModel.DataAnnotations;
using Raditap.BusinessLogic.ApiDataObjects.Common;

namespace Raditap.BusinessLogic.ValidationAttributes.Results
{
    public class GreaterThanOrEqualValidationResult : ValidationResult
    {
        public GreaterThanOrEqualValidationResult(string field, int value) : base(new GreaterThanOrEqualErrorResponse(field, value).ToString()) { }
    }
}