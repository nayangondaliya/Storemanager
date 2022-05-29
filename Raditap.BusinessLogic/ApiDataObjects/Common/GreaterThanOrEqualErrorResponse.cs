namespace Raditap.BusinessLogic.ApiDataObjects.Common
{
    public class GreaterThanOrEqualErrorResponse : ResponseBase<Result>
    {
        public GreaterThanOrEqualErrorResponse(string field, int value) : base(Result.GreaterThanOrEqualError.Value,
                                                                               string.Format(Result.GreaterThanOrEqualError.Name, field, value)) { }
    }
}