using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace LinqORM.Attributes
{
    public class PrimaryKeyAttribute : ColumnAttribute
    {
        public PrimaryKeyAttribute([CallerMemberName] string name = "") : base(name)
        {
        }
    }
}
