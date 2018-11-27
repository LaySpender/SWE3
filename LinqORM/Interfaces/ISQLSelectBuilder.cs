using System;
using System.Collections.Generic;
using System.Text;

namespace LinqORM.Interfaces
{
    /// <summary>
    /// Interface for building MSSQL select statements
    /// </summary>
    public interface ISQLSelectBuilder
    {
        string SelectStatement { get; }
    }
}
