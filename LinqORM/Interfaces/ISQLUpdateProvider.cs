using System;
using System.Collections.Generic;
using System.Text;

namespace LinqORM.Interfaces
{
    /// <summary>
    /// Interface for communication with mssql database regarding update statements.
    /// </summary>
    public interface ISQLUpdateProvider
    {
        void UpdateObject(object obj);
    }
}
