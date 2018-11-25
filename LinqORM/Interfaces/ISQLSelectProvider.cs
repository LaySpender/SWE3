using System;
using System.Collections.Generic;
using System.Text;

namespace LinqORM.Interfaces
{
    public interface ISQLSelectProvider
    {
        string Statement { get; }
        IEnumerable<Dictionary<string, object>> GetObjects();
    }
}
