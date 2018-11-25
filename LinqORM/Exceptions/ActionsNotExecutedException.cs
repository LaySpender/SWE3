using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace LinqORM.Exceptions
{
    internal class ActionsNotExecutedException : Exception
    {
        public ActionsNotExecutedException()
        {
        }

        public ActionsNotExecutedException(string message) : base(message)
        {
        }

        public ActionsNotExecutedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
