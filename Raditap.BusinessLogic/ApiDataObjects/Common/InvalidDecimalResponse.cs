namespace Raditap.BusinessLogic.ApiDataObjects.Common
{
    public class InvalidDecimalResponse : ResponseBase<Result>
    {
        public InvalidDecimalResponse(string field, int expectedPrecision, int expectedScale) : base(Result.InvalidDecimal.Value,
                                                                                                     string.Format(Result.InvalidDecimal.Name,
                                                                                                                   field,
                                                                                                                   expectedPrecision,
                                                                                                                   expectedScale)) { }
    }
}