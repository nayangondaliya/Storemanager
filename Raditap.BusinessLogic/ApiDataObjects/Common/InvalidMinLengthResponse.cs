namespace Raditap.BusinessLogic.ApiDataObjects.Common
{
    public class InvalidMinLengthResponse : ResponseBase<Result>
    {
        public InvalidMinLengthResponse(string field, int minLength) : base(Result.InvalidMinLength.Value,
                                                                            string.Format(Result.InvalidMinLength.Name, field, minLength)) { }
    }
}