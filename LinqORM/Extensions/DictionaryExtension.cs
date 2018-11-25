using System;
using System.Collections.Generic;
using System.Text;


namespace LinqORM.Extensions
{
    public static class DictionaryExtension
    {
        public static string GetEntriesForSQLUpdate<T, U>(this Dictionary<T, U> dict)
        {
            List<string> result = new List<string>();

            foreach (var entry in dict)
            {
                result.Add($"{entry.Key} = {entry.Value}");
            }
            return string.Join(',', result);
        }
    }
}
