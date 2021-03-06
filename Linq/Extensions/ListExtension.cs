﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqORM.Extensions
{
    public static class ListExtension
    {
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
