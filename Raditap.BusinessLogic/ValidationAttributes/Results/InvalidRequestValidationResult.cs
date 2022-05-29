using System.ComponentModel.DataAnnotations;
using Raditap.BusinessLogic.ApiDataObjects.Common;

namespace Raditap.BusinessLogic.ValidationAttributes.Results
{
    public class InvalidRequestValidationResult : ValidationResult
    {
        public InvalidRequestValidationResult() : base(new InvalidRequestResponse().ToString()) { }
    }
}