using System;
using System.Collections.Generic;
using System.Text;

namespace LinqORM.Interfaces
{
    /// <summary>
    /// Interface for communication with mssql database regarding delete statements.
    /// </summary>
    public interface ISQLDeleteProvider
    {
        void DeleteObject();
    }
}
