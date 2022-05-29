using Ardalis.SmartEnum;

namespace Raditap.BusinessLogic.ApiDataObjects
{
    public class Result : SmartEnum<Result, string>
    {
        //public ResultBase() { }

        public Result(string value, string name) : base(name, value) { }

        public static readonly Result Success = new Result("000", "Success");

        //  Global errors
        public static readonly Result Fail = new Result("999", "Failed");

        //  Error
        public static readonly Result InvalidRequest = new Result("I01", "Invalid request");
        public static readonly Result SystemError = new Result("E06", "System Error");

        public static readonly Result FieldIsRequired = new Result("E02", "'{0}' is required");
        public static readonly Result InvalidMaxLength = new Result("E03", "The length of '{0}' must be {1} characters or fewer");
        public static readonly Result InvalidMinLength = new Result("E04", "The length of '{0}' must be at least {1} characters");
        public static readonly Result InvalidEmailFormat = new Result("E05", "'{0}' email format is invalid");
        public static readonly Result RequiredOneOfValues = new Result("E06", "'{0}' must be one of the following, {1}");
        public static readonly Result InvalidDateTime = new Result("E08", "'{0}' is invalid date/time");
        public static readonly Result InvalidFieldValue = new Result("E09", "'{0}' is invalid");
        public static readonly Result BetweenError = new Result("E10", "'{0}' must be between {1} and {2}");
        public static readonly Result GreaterThanOrEqualError = new Result("E11", "'{0}' must be greater than or equal to {1}");
        public static readonly Result InvalidBoolean = new Result("E12", "'{0}' must be boolean");
        public static readonly Result InvalidDecimal = new Result("E13", "'{0}' must not be more than {1} digits in total, with allowance for {2} decimals");
        public static readonly Result InvalidExactLength = new Result("E14", "The length of '{0}' must be {1} characters");
        public static readonly Result InvalidAmount = new Result("E16", "Invalid amount");
        public static readonly Result CustomerTypeError = new Result("E24", "'{0}' {1} is invalid");


        //  Token
        public static readonly Result InvalidAccessToken = new Result("A01", "Invalid access token");
        public static readonly Result AccessTokenExpired = new Result("A04", "Access token expired");
        public static readonly Result Unauthorized = new Result("A03", "Unauthorized");
        public static readonly Result SessionExpired = new Result("A02", "Session expired");

        //  User
        public static readonly Result UserAlreadyExists = new Result("U02", "User Already Exists");
        public static readonly Result UserNotFound = new Result("U01", "User not found");

        //Data
        public static readonly Result DataAlreadyAvailable = new Result("D01", "Data already available");
        public static readonly Result DataNotFound = new Result("D02", "Data not found");
        public static readonly Result DatabaseError = new Result("D03", "Database Error");

        //Order
        public static readonly Result OrderNotFound = new Result("O01", "Order not found");
        public static readonly Result OrderAlreadyExists = new Result("O02", "Order number already exists");

        //Job
        public static readonly Result JobNotFound = new Result("J01", "Job not found");
        public static readonly Result JobAlreadyExists = new Result("J02", "Job number already exists");

        public static readonly Result InvalidLoginCredentials = new Result("I03", "Invalid login credentials");

        public bool IsSuccess()
        {
            return Value == Success.Value;
        }
    }
}
