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


        public static string Remove(this string input, string valueToSearch)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(valueToSearch))
                return input;

            int startPos = input.IndexOf(valueToSearch, StringComparison.OrdinalIgnoreCase) + valueToSearch.Length;

            return startPos == -1 ? input : input.Remove(startPos);
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

        public static string TrimStart(this string input, string word)
        {
            var index = input.IndexOf(word);
            if (index == -1) return input;
            if (index == 0)
            {
                return input.Remove(word.Length);
            }

            return input;

        }

        public static string Between(this string inputString, string firstString, string secondString)
        {
            if (string.IsNullOrEmpty(inputString) || string.IsNullOrEmpty(firstString) || string.IsNullOrEmpty(secondString))
                return string.Empty;

            var firstOccur = inputString.IndexOf(firstString, StringComparison.OrdinalIgnoreCase);
            if (firstOccur == -1) return string.Empty;

            var secondOccur = inputString.IndexOf(secondString, firstOccur, StringComparison.OrdinalIgnoreCase);
            if (secondOccur == -1) return string.Empty;
            if (firstOccur > secondOccur) return string.Empty;


            int startPos = (inputString.IndexOf(firstString, StringComparison.OrdinalIgnoreCase) + firstString.Length);
            int endPos = inputString.IndexOf(secondString, startPos, StringComparison.OrdinalIgnoreCase);

            return inputString.Substring(startPos, endPos - startPos).Trim();
        }
        public static bool ToBool(this string input, bool defaultValue)
        {
            if (string.IsNullOrEmpty(input)) return defaultValue;

            if (input == "1")
            {
                return true;
            }

            if (input == "0")
            {
                return false;
            }

            return bool.TryParse(input, out var exist) ? exist : defaultValue;
        }
    }
}
