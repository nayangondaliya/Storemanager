namespace Raditap.BusinessLogic.ApiDataObjects.Common
{
    public class InvalidAmountResponse : ResponseBase<Result>
    {
        public InvalidAmountResponse() : base(Result.InvalidAmount) { }
    }
}