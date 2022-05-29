namespace Raditap.BusinessLogic.ApiDataObjects.Common
{
    public class InvalidFieldValueResponse : ResponseBase<Result>
    {
        public InvalidFieldValueResponse(string field) : base(Result.InvalidFieldValue.Value, string.Format(Result.InvalidFieldValue.Name, field)) { }
    }
}