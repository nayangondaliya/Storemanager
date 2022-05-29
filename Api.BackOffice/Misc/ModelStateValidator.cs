using System;
using System.Linq;
using Raditap.BusinessLogic.ApiDataObjects.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Raditap.Api.Misc
{
    public class ModelStateValidator
    {
        public static IActionResult ValidateModelState(ActionContext context)
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<ModelStateValidator>>();

            try
            {
                (string fieldName, ModelStateEntry entry) = context.ModelState.First(x => x.Value.Errors.Count > 0);
                var errorSerialized = JsonConvert.DeserializeObject<dynamic>(entry.Errors.First().ErrorMessage);

                logger.LogDebug($"Request field name: {fieldName} is invalid");
                var result = new OkObjectResult(errorSerialized);

                return result;
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error occured while preparing request validation message");
                return new OkObjectResult(new InternalServerErrorResponse());
            }
        }
    }
}