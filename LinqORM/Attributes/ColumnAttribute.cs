using System;
using System.Runtime.CompilerServices;

namespace LinqORM.Attributes
{
    public class ColumnAttribute : Attribute
    {
        public string Name { get; private set; }

        public ColumnAttribute([CallerMemberName] string name = "")
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException();
            Name = name;
        }
    }
}