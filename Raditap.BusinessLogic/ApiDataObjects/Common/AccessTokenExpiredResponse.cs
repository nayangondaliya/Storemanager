namespace Raditap.BusinessLogic.ApiDataObjects.Common
{
    public class AccessTokenExpiredResponse : ResponseBase<Result>
    {
        public AccessTokenExpiredResponse() : base(Result.AccessTokenExpired) { }
    }
}