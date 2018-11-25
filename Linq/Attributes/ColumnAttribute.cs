using System;

namespace LinqORM.Attributes
{
    internal class ColumnAttribute : Attribute
    {
        public string Name { get; set; } = "";
    }
}