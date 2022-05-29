namespace Raditap.BusinessLogic.ApiDataObjects.Common
{
    public class BetweenErrorResponse : ResponseBase<Result>
    {
        public BetweenErrorResponse(string field, int min, int max) : base(Result.BetweenError.Value, string.Format(Result.BetweenError.Name, field, min, max)) { }
    }
}