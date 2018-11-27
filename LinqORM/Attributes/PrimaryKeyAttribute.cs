using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace LinqORM.Attributes
{
    /// <summary>
    /// Labels the property as the primary key of the table.
    /// </summary>
    /// <seealso cref="LinqORM.Attributes.ColumnAttribute" />
    public class PrimaryKeyAttribute : ColumnAttribute
    {
        public PrimaryKeyAttribute([CallerMemberName] string name = "") : base(name)
        {
        }
    }
}
