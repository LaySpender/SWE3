using System;
using System.Collections.Generic;
using System.Text;

namespace LinqORM.Helpers
{
    internal class ValueFormatter
    {
        internal static string FormatForQuery(object obj)
        {
            if (obj.GetType() == typeof(string))
            {
                return $"'{obj}'";
            }
            return $"{obj}";
        }
    }
}
