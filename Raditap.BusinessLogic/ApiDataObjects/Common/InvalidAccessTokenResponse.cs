namespace Raditap.BusinessLogic.ApiDataObjects.Common
{
    public class InvalidAccessTokenResponse : ResponseBase<Result>
    {
        public InvalidAccessTokenResponse() : base(Result.InvalidAccessToken) { }
    }
}