using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Raditap.BusinessLogic.ApiDataObjects
{
    public abstract class ValidatableRequestBase : RequestBase, IValidatableObject
    {
        public abstract IEnumerable<ValidationResult> Validate(ValidationContext validationContext);
    }
}
