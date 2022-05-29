namespace Raditap.BusinessLogic.ApiDataObjects.Common
{
    public class SessionExpiredResponse : ResponseBase<Result>
    {
        public SessionExpiredResponse() : base(Result.SessionExpired) { }
    }
}