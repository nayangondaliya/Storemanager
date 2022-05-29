namespace Raditap.BusinessLogic.ApiDataObjects.Common
{
    public class CustomerTypeErrorResponse : ResponseBase<Result>
    {
        public CustomerTypeErrorResponse(string field, string value) : base(Result.CustomerTypeError.Value,
            string.Format(Result.CustomerTypeError.Name, field, value))
        { }
    }
}