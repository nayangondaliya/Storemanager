using System;

namespace Raditap.BusinessLogic.Contexts
{
    public class LoginUserData
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? MobileNumber { get; set; }
        public string Email { get; set; }
    }
}
