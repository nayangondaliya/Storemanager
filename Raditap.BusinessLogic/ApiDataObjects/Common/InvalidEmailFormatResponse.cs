namespace Raditap.BusinessLogic.ApiDataObjects.Common
{
    public class InvalidEmailFormatResponse : ResponseBase<Result>
    {
        public InvalidEmailFormatResponse(string field) : base(Result.InvalidEmailFormat.Value, string.Format(Result.InvalidEmailFormat.Name, field)) { }
    }
}