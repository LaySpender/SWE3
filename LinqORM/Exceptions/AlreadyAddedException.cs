using System;
using System.Collections.Generic;
using System.Text;

namespace LinqORM.Exceptions
{
    public class AlreadyAddedException : Exception
    {
        public AlreadyAddedException()
        {
        }

        public AlreadyAddedException(string message) : base(message)
        {
        }

        public AlreadyAddedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
