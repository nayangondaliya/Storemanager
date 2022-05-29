namespace Raditap.BusinessLogic.ApiDataObjects.Common
{
    public class InvalidRequestResponse: ResponseBase<Result>
    {
        public InvalidRequestResponse() : base(Result.InvalidRequest) { }
    }
}