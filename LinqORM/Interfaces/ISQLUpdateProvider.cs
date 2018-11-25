using System;
using System.Collections.Generic;
using System.Text;

namespace LinqORM.Interfaces
{
    public interface ISQLUpdateProvider
    {
        void UpdateObject(object obj);
    }
}
