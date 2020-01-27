using System;
using System.Collections.Generic;
using System.Reflection;

namespace WLib.Core.Services.ObjectPropertyParsers
{
    public static class BusinessObjectParser
    {
        private static readonly Dictionary<Type, Type> StripProxyDefinitionCash = new Dictionary<Type, Type>();






        /// <summary>
        ///     Checks if type is value or string type
        /// </summary>
        /// <returns>True if property is string. If property contains "." return false, because it is not value type then</returns>
        public static bool IsValueTypeOrString(Type type, string propertyPath)
        {
            if (propertyPath.IndexOf('.') != -1)
                return false;

            var propType = type.GetProperty(propertyPath)?.PropertyType;

            if (propType.IsValueType || propType == typeof(string))
                return true;

            return false;
        }

        /// <summary>
        ///     Checks if type is value or string type
        /// </summary>
        /// <returns>True if property is string</returns>
        public static bool IsValueTypeOrString(PropertyInfo pi)
        {
            if (pi.PropertyType.IsValueType || pi.PropertyType == typeof(string))
                return true;

            return false;
        }



        /// <summary>
        ///     Our NHibernate layer sometimes return proxy objects instead of
        ///     real objects. This methods returns the base type if it is proxy
        ///     object, and current type, if it is not
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type StripProxyDefinition(Type type)
        {
            if (type == null)
                throw new ArgumentNullException();
            Type cashedType;
            StripProxyDefinitionCash.TryGetValue(type, out cashedType);
            if (cashedType != null)
                return cashedType;
            if (type.FullName.Contains("Proxy"))
            {
                StripProxyDefinitionCash.Add(type, type.BaseType);
                return type.BaseType;
            }

            StripProxyDefinitionCash.Add(type, type);
            return type;
        }
    }
}