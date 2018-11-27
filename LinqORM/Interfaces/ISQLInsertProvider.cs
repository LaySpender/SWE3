using System;
using System.Collections.Generic;
using System.Text;

namespace LinqORM.Interfaces
{
    /// <summary>
    /// Interface for communication with mssql database regarding insert statements.
    /// </summary>
    public interface ISQLInsertProvider
    {
        void InsertObject(object obj);
    }
}
