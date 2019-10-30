using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WLib.Core.Bll.Extensions
{
    public static class StringExtentions
    {
        public static string FirstCharToUpper(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": return "";
                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }

        /// <summary>
        /// Using Convert.ToDateTime(input);
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultDate"></param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this string input, DateTime? defaultDate = null, IFormatProvider formatProvider = null, bool throwOnError = false)
        {
            DateTime? result = defaultDate;

            try
            {
                result = Convert.ToDateTime(input, formatProvider);
            }
            catch (Exception)
            {
                try
                {
                    double d = double.Parse(input);
                    result = DateTime.FromOADate(d);
                }
                catch
                {
                    if (throwOnError)
                    {
                        throw;
                    }
                }

            }

            return result;
        }

        /// <summary>
        /// Two cats cats running. If pass "cats " returns "running."
        /// </summary>
        /// <param name="input"></param>
        /// <param name="stringToSearch"></param>
        /// <returns></returns>
        public static string SubStringFromEnd(this string input, string stringToSearch)
        {
            int lastIndexOfWord = input.LastIndexOf(stringToSearch, StringComparison.InvariantCulture);
            if (lastIndexOfWord > -1)
            {
                return input.Remove(0, lastIndexOfWord + stringToSearch.Length);
            }

            return input;
        }
        /// <summary>
        /// Two cats cats running. If pass "cats " returns " cats running."
        /// </summary>
        /// <param name="input"></param>
        /// <param name="startingWith"></param>
        /// <returns></returns>
        public static string SubstringFromStart(this string input, string startingWith)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(startingWith))
                return string.Empty;

            int startPos = input.IndexOf(startingWith, StringComparison.OrdinalIgnoreCase) + startingWith.Length;
            return startPos == -1 ? string.Empty : input.Substring(startPos);
        }

        public static decimal? ToDecimal(this string input, decimal? defaultValue)
        {
            if (string.IsNullOrEmpty(input)) return defaultValue;

            if (input.IndexOf(".") > -1)
            {
                var isDecimal = input.Substring(input.IndexOf(".")).Replace(".", string.Empty);
                if (isDecimal.Length >= 1)
                {
                    return ToDecimal(input.Replace(".", ","), defaultValue);

                }
            }


            return !decimal.TryParse(input, out var result) ? defaultValue : result;
        }

        public static int? ToInt(this string input, int? defaultValue)
        {
            if (string.IsNullOrEmpty(input)) return defaultValue;

            return int.TryParse(input, out var result) ? result : defaultValue;
        }

        public static bool IsInt(this string input)
        {
            return int.TryParse(input, out var number);
        }

        public static bool ToBool(this string input, bool defaultValue)
        {
            if (string.IsNullOrEmpty(input)) return defaultValue;

            return bool.TryParse(input, out var exist) ? exist : defaultValue;
        }
    }
}
