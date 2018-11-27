using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqORM.Extensions
{
    /// <summary>
    /// Class for List extension
    /// </summary>
    public static class ListExtension
    {
        /// <summary>
        /// Gets the entries seperated by.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="seperator">The seperator.</param>
        /// <returns></returns>
        public static string GetEntriesSeperatedBy<T>(this List<T> list, string seperator)
        {
            string result = list[0].ToString();

            for (int i = 1; i < list.Count; i++)
            {
                T entry = list[i];
                result += seperator + entry.ToString();
            }

            return result;
        }        
    }
}
