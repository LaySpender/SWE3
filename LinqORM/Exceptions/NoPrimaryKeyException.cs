using System;
using System.Collections.Generic;
using System.Text;

namespace LinqORM.Exceptions
{
    /// <summary>
    /// no primary key has been found.
    /// </summary>
    /// <seealso cref="System.Exception" />
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
