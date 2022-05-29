using System.ComponentModel.DataAnnotations;
using Raditap.BusinessLogic.ApiDataObjects.Common;

namespace Raditap.BusinessLogic.ValidationAttributes.Results
{
    public class FieldIsRequiredValidationResult : ValidationResult
    {
        public FieldIsRequiredValidationResult(string field) : base(new FieldIsRequiredResponse(field).ToString()) { }
    }
}