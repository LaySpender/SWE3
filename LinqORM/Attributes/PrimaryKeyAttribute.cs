using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace LinqORM.Attributes
{
    public class PrimaryKeyAttribute : Attribute
    {
        public string Name { get; private set; }

        public PrimaryKeyAttribute([CallerMemberName] string name = "")
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException();
            Name = name;
        }
    }
}
