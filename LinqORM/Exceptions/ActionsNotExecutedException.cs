using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace LinqORM.Exceptions
{
    /// <summary>
    /// after each action (attach, delete) the saveChanges method has to be called.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class ActionsNotExecutedException : Exception
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
