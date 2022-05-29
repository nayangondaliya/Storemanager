using System;
using Newtonsoft.Json;
using Raditap.BusinessLogic.ApiDataObjects;

namespace Raditap.BusinessLogic.Exceptions
{
    public class GeneralNotFoundExceptionResponse : ResponseBase<Result>
    {
        public GeneralNotFoundExceptionResponse(Result result) : base(result) { }
    }

    public class GeneralNotFoundException : Exception
    {
        public GeneralNotFoundException(Result result) : base(JsonConvert.SerializeObject(new GeneralNotFoundExceptionResponse(result))) { }
    }
}
