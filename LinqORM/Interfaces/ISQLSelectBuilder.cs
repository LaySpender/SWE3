using System;
using System.Collections.Generic;
using System.Text;

namespace LinqORM.Interfaces
{
    public interface ISQLSelectBuilder
    {
        string SelectStatement { get; }
    }
}
