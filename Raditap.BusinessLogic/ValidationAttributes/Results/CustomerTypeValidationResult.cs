using System.ComponentModel.DataAnnotations;
using Raditap.BusinessLogic.ApiDataObjects.Common;

namespace Raditap.BusinessLogic.ValidationAttributes.Results
{
    public class CustomerTypeValidationResult : ValidationResult
    {
        public CustomerTypeValidationResult(string field, string value) : base(new CustomerTypeErrorResponse(field, value).ToString()) { }
    }
}