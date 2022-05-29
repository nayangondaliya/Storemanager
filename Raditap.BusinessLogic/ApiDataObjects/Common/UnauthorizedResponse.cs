namespace Raditap.BusinessLogic.ApiDataObjects.Common
{
    public class UnauthorizedResponse : ResponseBase<Result>
    {
        public UnauthorizedResponse() : base(Result.Unauthorized) { }
    }
}
