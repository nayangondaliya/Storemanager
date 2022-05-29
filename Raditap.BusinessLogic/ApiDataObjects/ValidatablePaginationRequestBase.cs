using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Raditap.BusinessLogic.ApiDataObjects
{
    public abstract class ValidatablePaginationRequestBase : PaginationRequestBase, IValidatableObject
    {
        public abstract IEnumerable<ValidationResult> Validate(ValidationContext validationContext);
    }
}
