using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using WLib.Core.Bll.Model.Meta;

namespace WLib.Core.Services.SystemServices
{
    public class ValueParser
    {
        public static T ConvertTo<T>(object value)
        {
            object convertedValue = ConvertTo(value, typeof(T));
            if (convertedValue is T)
                return (T)convertedValue;
            return default(T);
        }

        public static T ConvertTo<T>(object value, bool throwException)
        {
            object convertedValue = ConvertTo(value, typeof(T), throwException);
            if (convertedValue == null)
                return default(T);
            return (T)convertedValue;
        }

        public static object ConvertTo(object value, Type typeToConvertTo)
        {
            return ConvertTo(value, typeToConvertTo, true);
        }

        public static object Default(Type targetType)
        {
            if (targetType.Equals(typeof(string)))
                return string.Empty;
            return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;
        }

        public static T Default<T>()
        {
            // we do not use keyword default,
            // since it returns null for string as default value
            // we want string.Empty to be default value for strings
            return (T)Default(typeof(T));
        }

        /// <summary>
        /// Useful together with IFilterRule to get filter rule value
        /// based on value type
        /// </summary>
        /// <param name="value"></param>
        /// <param name="typeToConvertTo">if value == null, return the same object</param>
        /// <param name="throwException">if true, throws exception
        /// if value can not be converted. Else, returns null</param>
        /// <returns></returns>
        public static object ConvertTo(object value, Type typeToConvertTo,
                                       bool throwException)
        {
            if (typeToConvertTo == null)
                return value;
            if (value == null)
                return Default(typeToConvertTo);

            if ((string.IsNullOrEmpty(value.ToString()) == true) && (value is IEntity == false))
                return Default(typeToConvertTo);
            string type = typeToConvertTo.FullName.ToLower();
            object ret = value;
            try
            {
                if (type.Contains("string"))
                    ret = value.ToString();
                else if (type.Contains("bool"))
                {
                    ret = System.Convert.ToBoolean(value);
                }
                else if (type.Contains("datetime"))
                {
                    ret = System.Convert.ToDateTime(value);
                }
                else if (type.Contains("double"))
                {
                    NumberFormatInfo numberFormat = CultureInfo.InvariantCulture.NumberFormat;
                    if (value is string)
                    {
                        string valueString = (string)value;
                        valueString = valueString.Replace(",", ".");
                        ret = System.Convert.ToDouble(valueString, numberFormat);
                    }
                    else
                        ret = System.Convert.ToDouble(value, numberFormat);
                }
                else
                    if (type.Contains("decimal"))
                {
                    NumberFormatInfo numberFormat = CultureInfo.InvariantCulture.NumberFormat;
                    if (value is string)
                    {
                        string valueString = (string)value;
                        valueString = valueString.Replace(",", ".");
                        ret = System.Convert.ToDecimal(valueString, numberFormat);
                    }
                    else
                        ret = System.Convert.ToDecimal(value, numberFormat);
                }
                else if (type.Contains("int"))
                {
                    ret = System.Convert.ToInt32(value);
                }
                else if (typeof(IEntity).IsAssignableFrom(typeToConvertTo))
                {
                    if (value is IEntity)
                        return ret;
                    try
                    {
                        ret = System.Convert.ToInt32(value);
                    }
                    catch (FormatException e)
                    {
                        throw new FormatException($"You asked to convert value {value.ToString()} to {typeToConvertTo.FullName}, but i can not do it," +
                            " since your value is not an integer", e);
                    }
                }
            }
            catch (Exception e)
            {
                if (throwException == false)
                    return null;
                throw new FormatException($"Can not convert value of '{value.ToString()}' to value of type '{typeToConvertTo.FullName}'", e);
            }
            return ret;
        }
    }
}