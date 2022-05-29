using System;

namespace Raditap.Utilities.Exceptions
{
    public class AesFieldException : Exception
    {
        public AesFieldException(string field) : base(field) { }
    }
}
