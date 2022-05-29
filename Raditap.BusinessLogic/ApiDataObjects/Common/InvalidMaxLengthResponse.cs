namespace Raditap.BusinessLogic.ApiDataObjects.Common
{
    public class InvalidMaxLengthResponse : ResponseBase<Result>
    {
        public InvalidMaxLengthResponse(string field, int maxLength) : base(Result.InvalidMaxLength.Value,
                                                                            string.Format(Result.InvalidMaxLength.Name, field, maxLength)) { }
    }
}