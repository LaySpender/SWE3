using System;
using System.Collections.Generic;
using System.Text;

namespace LinqORM.Helpers
{
    /// <summary>
    /// Class for formatting values for mssql queries.
    /// </summary>
    public class ValueFormatter
    {
        /// <summary>
        /// Formats for query.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static string FormatForQuery(object obj)
        {
            if (obj.GetType() == typeof(string))
            {
                return $"'{obj}'";
            }
            return $"{obj}";
        }
    }
}
