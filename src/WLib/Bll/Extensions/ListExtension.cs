using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WLib.Core.Bll.Extensions
{
    public static class ListExtension
    {
        public static IEnumerable<string> FindDuplicates(this IEnumerable<string> input)
        {
            return input.GroupBy(x => x)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key);
        }

        public static void RemoveRange<T>(this ICollection<T> input, IEnumerable<T> itemsToRemove)
        {
            foreach (var x1 in itemsToRemove)
            {
                input.Remove(x1);
            }
        }

        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> list, int parts)
        {
            var aa = list
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / parts)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();

            int ii = 0;
            var splits = from item in list
                         group item by ii++ % parts into part
                         select part.AsEnumerable();
            return aa;
        }

        public static string ToStringJoin(this IEnumerable<int> input, string seperator = ",")
        {
            var joinedString = string.Join(seperator, input.Distinct());

            return joinedString;
        }
        public static string ToStringJoin(this IEnumerable<string> input, string seperator = ",")
        {
            //var joinedString = string.Join(seperator, input);

            var sb = new StringBuilder();
            bool firstRun = true;
            var uniqueLines = input.Distinct();
            foreach (var val in uniqueLines)
            {
                if (!firstRun)
                {
                    sb.Append(seperator);
                }

                firstRun = false;
                sb.Append("'").Append(val).Append("'");
            }

            return sb.ToString();
        }
    }
}
