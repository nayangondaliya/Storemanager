namespace Raditap.BusinessLogic.ApiDataObjects.Common
{
    public class SuccessResponse : ResponseBase<Result>
    {
        public SuccessResponse() : base(Result.Success) { }
    }
}