namespace Raditap.BusinessLogic.ApiDataObjects.Common
{
    public class InvalidDateTimeResponse : ResponseBase<Result>
    {
        public InvalidDateTimeResponse(string field) : base(Result.InvalidDateTime.Value, string.Format(Result.InvalidDateTime.Name, field)) { }
    }
}