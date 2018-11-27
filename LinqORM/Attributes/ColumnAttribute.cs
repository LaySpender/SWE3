using System;
using System.Runtime.CompilerServices;

namespace LinqORM.Attributes
{
    /// <summary>
    /// Labels a property as being a column in the database table.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public class ColumnAttribute : Attribute
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnAttribute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <exception cref="ArgumentException"></exception>
        public ColumnAttribute([CallerMemberName] string name = "")
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException();
            Name = name;
        }
    }
}