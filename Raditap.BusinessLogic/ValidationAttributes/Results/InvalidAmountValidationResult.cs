using System.ComponentModel.DataAnnotations;
using Raditap.BusinessLogic.ApiDataObjects.Common;

namespace Raditap.BusinessLogic.ValidationAttributes.Results
{
    public class InvalidAmountValidationResult : ValidationResult
    {
        public InvalidAmountValidationResult() : base(new InvalidAmountResponse().ToString()) { }
    }
}