namespace Raditap.BusinessLogic.ApiDataObjects.Common
{
    public class FieldIsRequiredResponse : ResponseBase<Result>
    {
        public FieldIsRequiredResponse(string field) : base(Result.FieldIsRequired.Value, string.Format(Result.FieldIsRequired.Name, field)) { }
    }
}