using System;
using System.Collections.Generic;
using System.Text;

namespace LinqORM.Exceptions
{
    public class NoPrimaryKeyException : Exception
    {
        public NoPrimaryKeyException()
        {
        }

        public NoPrimaryKeyException(string message) : base(message)
        {
        }

        public NoPrimaryKeyException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
