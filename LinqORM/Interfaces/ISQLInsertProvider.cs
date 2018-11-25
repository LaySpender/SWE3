using System;
using System.Collections.Generic;
using System.Text;

namespace LinqORM.Interfaces
{
    public interface ISQLInsertProvider
    {
        void InsertObject(object obj);
    }
}
