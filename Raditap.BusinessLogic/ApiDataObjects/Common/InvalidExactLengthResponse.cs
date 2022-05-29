namespace Raditap.BusinessLogic.ApiDataObjects.Common
{
    public class InvalidExactLengthResponse : ResponseBase<Result>
    {
        public InvalidExactLengthResponse(string field, int length) : base(Result.InvalidExactLength.Value,
                                                                           string.Format(Result.InvalidExactLength.Name, field, length)) { }
    }
}