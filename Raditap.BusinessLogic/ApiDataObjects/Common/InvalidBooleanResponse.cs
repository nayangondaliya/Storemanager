namespace Raditap.BusinessLogic.ApiDataObjects.Common
{
    public class InvalidBooleanResponse : ResponseBase<Result>
    {
        public InvalidBooleanResponse(string field) : base(Result.InvalidBoolean.Value, string.Format(Result.InvalidBoolean.Name, field)) { }
    }
}