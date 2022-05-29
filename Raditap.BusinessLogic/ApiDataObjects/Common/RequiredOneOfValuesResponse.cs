namespace Raditap.BusinessLogic.ApiDataObjects.Common
{
    public class RequiredOneOfValuesResponse : ResponseBase<Result>
    {
        public RequiredOneOfValuesResponse(string field, string listOfValues) : base(Result.RequiredOneOfValues.Value,
                                                                                     string.Format(Result.RequiredOneOfValues.Name, field, listOfValues)) { }
    }
}