using System;
using System.Collections.Generic;
using System.Text;

namespace LinqORM.Interfaces
{
    /// <summary>
    /// Interface for communication with mssql database regarding select statements.
    /// </summary>
    public interface ISQLSelectProvider
    {
        string Statement { get; }
        IEnumerable<Dictionary<string, object>> GetObjects();
    }
}
