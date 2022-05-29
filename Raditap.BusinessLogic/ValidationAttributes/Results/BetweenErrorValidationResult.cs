using System.ComponentModel.DataAnnotations;
using Raditap.BusinessLogic.ApiDataObjects.Common;

namespace Raditap.BusinessLogic.ValidationAttributes.Results
{
    public class BetweenErrorValidationResult : ValidationResult
    {
        public BetweenErrorValidationResult(string field, int min, int max) : base(new BetweenErrorResponse(field, min, max).ToString()) { }
    }
}