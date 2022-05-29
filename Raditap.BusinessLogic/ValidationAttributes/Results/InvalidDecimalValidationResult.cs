using System.ComponentModel.DataAnnotations;
using Raditap.BusinessLogic.ApiDataObjects.Common;

namespace Raditap.BusinessLogic.ValidationAttributes.Results
{
    public class InvalidDecimalValidationResult : ValidationResult
    {
        public InvalidDecimalValidationResult(string field, int expectedPrecision, int expectedScale) :
                base(new InvalidDecimalResponse(field, expectedPrecision, expectedScale).ToString()) { }
    }
}