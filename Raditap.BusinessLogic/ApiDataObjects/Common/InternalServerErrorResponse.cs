namespace Raditap.BusinessLogic.ApiDataObjects.Common
{
    public class InternalServerErrorResponse : ResponseBase<Result>
    {
        public InternalServerErrorResponse() : base(Result.SystemError) { }
    }
}