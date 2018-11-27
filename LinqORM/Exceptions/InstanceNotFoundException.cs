using System;
using System.Collections.Generic;
using System.Text;

namespace LinqORM.Exceptions
{
    /// <summary>
    /// no instance has been found.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class InstanceNotFoundException : Exception
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
