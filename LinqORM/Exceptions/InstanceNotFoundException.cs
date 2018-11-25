using System;
using System.Collections.Generic;
using System.Text;

namespace LinqORM.Exceptions
{
    class InstanceNotFoundException : Exception
    {
        public InstanceNotFoundException()
        {
        }

        public InstanceNotFoundException(string message) : base(message)
        {
        }

        public InstanceNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
