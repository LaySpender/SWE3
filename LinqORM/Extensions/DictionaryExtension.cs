using System;
using System.Collections.Generic;
using System.Text;


namespace LinqORM.Extensions
{
    /// <summary>
    /// Class for Dictionary extensions
    /// </summary>
    public static class DictionaryExtension
    {
        /// <summary>
        /// Gets the string for the entries for SQL update.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="dict">The dictionary.</param>
        /// <returns></returns>
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
