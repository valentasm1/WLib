using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using WLib.Core.Services.Exceptions;
using WLib.Core.Services.ObjectPropertyParsers;

namespace WLib.Core.Services.SystemServices
{
    public static class ReflectionPropertyAccess
    {
        //private static log4net.ILog m_log = log4net.LogManager.GetLogger(typeof(ReflectionPropertyAccess));

        //public static readonly Dictionary<Tuple<Type, string>, PropertyInfo> PropertyCache = new Dictionary<Tuple<Type, string>, PropertyInfo>();
        //private static readonly Dictionary<Tuple<Type, string>, MethodInfo> MethodCache = new Dictionary<Tuple<Type, string>, MethodInfo>();

        /// <summary>
        /// Returns PropertyInfo from given property path.
        /// </summary>
        /// <param name="type">type is stripped from proxy defintion, if any</param>
        /// <param name="path"></param>
        /// <returns>NullReferenceException if not found :)</returns>
        public static PropertyInfo GetPropertyInfoFromPath(Type type, string path)
        {
            type = BusinessObjectParser.StripProxyDefinition(type);

            //split property path into parts
            string[] parts = path.Split('.');

            System.Reflection.PropertyInfo pi = null;
            System.Type currentType = type;
            foreach (string part in parts)
            {
                pi = GetTypeProperty(currentType, part);
                if (pi == null)
                    throw new PropertyNotFoundException(currentType, path);
                currentType = pi.PropertyType;
            }
            return pi;
        }

        /// <summary>
        /// True, if path is valid
        /// </summary>
        /// <param name="type"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsValidPropertyPath(Type type, string path)
        {
            try
            {
                return GetPropertyInfoFromPath(type, path) != null;
            }
            catch (PropertyNotFoundException)
            {
                return false;
            }
        }

        public static PropertyInfo GetTypeProperty(Type currentType, string part)

        {
            currentType = BusinessObjectParser.StripProxyDefinition(currentType);

            PropertyInfo pi = null;
            try
            {
                pi = currentType.GetProperty(part);
                return pi;
            }
            catch (AmbiguousMatchException)
            {
                PropertyInfo[] pis = currentType.GetProperties();
                foreach (PropertyInfo piInPis in pis)
                {
                    if (piInPis.Name.Equals(part))
                    {
                        if (piInPis.Module.FullyQualifiedName.Contains("DynamicProxyGenAssembly2")) //DynamicProxyGenAssembly2
                            continue;
                        return piInPis;
                    }
                }
                throw new ArgumentException("For current type '" + currentType.FullName + "' property '" + part + "' was not found");
            }
        }

        /// <summary>
        /// Returns value from the given object for the given path.
        /// </summary>
        /// <remarks>Supports function calls.</remarks>
        /// <param name="item"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static object GetValueFromPath(object item, string path)
        {
            return GetValueFromPathNoCache(item, path);
        }



        /// <summary>
        /// (NO CACHE) Returns value from the given object for the given path.
        /// </summary>
        /// <remarks>Supports function calls.</remarks>
        /// <param name="item"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private static object GetValueFromPathNoCache(object item, string path)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            //split property path into parts
            string[] parts = path.Split('.');

            Type currentType = item.GetType();
            currentType = BusinessObjectParser.StripProxyDefinition(currentType);
            Type origType = currentType;
            object currentValue = item;

            foreach (string part in parts)
            {
                if (currentValue == null)
                {
                    PropertyInfo pi = ReflectionPropertyAccess.GetPropertyInfoFromPath(origType, path);
                    return ValueParser.ConvertTo(null, pi.PropertyType, false);
                }
                if (-1 == part.IndexOf('(')) // is not a method
                {
                    // Search for property
                    PropertyInfo pi = GetTypeProperty(currentType, part);
                    if (pi == null)
                        throw new ArgumentException("Property " + path + " not found in object " + item.ToString());
                    currentType = pi.PropertyType;
                    if (currentValue != null)
                        currentValue = pi.GetValue(currentValue, null);
                }
                else
                {
                    // Search for method
                    int paramBegin = part.IndexOf('(');
                    int paramEnd = part.IndexOf(')');
                    string methodName = part.Substring(0, paramBegin);
                    MethodInfo mi = currentType.GetMethod(methodName);

                    // Parsing parameters
                    string paramStr = part.Substring(paramBegin + 1, paramEnd - paramBegin - 1);
                    string[] paramLst = paramStr.Split(',');
                    object[] paramValues = new object[paramLst.Length];
                    for (int i = 0; i < paramLst.Length; i++)
                        paramValues[i] = GetValueFromPath(item, paramLst[i]);

                    // Invoke
                    currentValue = mi.Invoke(currentValue, paramValues);
                }
            }
            return currentValue;
        }

        /// <summary>
        /// Returns property type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="path"></param>
        /// <returns>null, if path is not valid</returns>
        public static Type GetPropertyType(Type type, string path)
        {
            System.Reflection.PropertyInfo pi = GetPropertyInfoFromPath(type, path);
            if (pi == null)
                return null;
            return pi.PropertyType;
        }

        /// <summary>
        /// Searches for PropertyInfo of given object type from
        /// the base type.
        /// </summary>
        /// <param name="baseType"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static PropertyInfo GetPropertyInfoFromType(System.Type baseType, System.Type type)
        {
            PropertyInfo[] properties = baseType.GetProperties();
            foreach (PropertyInfo pi in properties)
            {
                object[] attributeList = null;
                if (pi.PropertyType == type)
                    return pi;
            }
            return null;
        }

        /// <summary>
        /// Returns true, if object of type objectType is accessible
        /// from baseType. Returns true if baseType equals objectType
        /// </summary>
        /// <param name="baseType"></param>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public static bool IsAccessibleTypeFromType(Type baseType, Type objectType)
        {
            if (baseType == objectType)
                return true;
            PropertyInfo pi = GetPropertyInfoFromType(baseType, objectType);
            if (pi == null)
                return false;
            return true;
        }



        public static void SetValueFromPath(object item, object newValue, string path)
        {
            SetValueFromPath(item, newValue, path, false);
        }
        /// <summary>
        /// Sets value using property path
        /// </summary>
        /// <param name="item"></param>
        /// <param name="newValue"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static void SetValueFromPath(object item, object newValue, string path, bool castedValue)
        {
            if (item == null)
                throw new ArgumentNullException("object, on which you want to change value, can not be null");
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("object property, on which you want to change value, can not be null");

            object baseItem = item;
            string basePath = PropertyParser.ClassParser.GetBasePath(path);
            if (string.IsNullOrEmpty(basePath) == false)
                baseItem = ReflectionPropertyAccess.GetValueFromPath(item, basePath);

            if (baseItem == null)
                return;

            PropertyInfo pi = GetPropertyInfoFromPath(item.GetType(), path);
            if (pi == null)
                throw new MissingFieldException(item.GetType().FullName, path);
            if (pi.CanWrite)
            {
                try
                {
                    pi.SetValue(baseItem, newValue, null);
                }
                catch (TargetInvocationException e)
                {
                    if (e.InnerException is ArgumentOutOfRangeException)
                    {
                        return;
                    }

                    throw new TargetInvocationException($"Can not set value to item {item} to property {path}." + $"value is {newValue}", e);
                }
                catch (ArgumentException ex)
                {

                    if (!castedValue)
                    {
                        if (pi.PropertyType.IsAssignableFrom(typeof(decimal)) || pi.PropertyType.IsAssignableFrom(typeof(decimal?)))
                        {
                            var newCastedValue = Convert.ToDecimal(newValue);
                            SetValueFromPath(item, newCastedValue, path, true);
                        }

                        if (pi.PropertyType.IsAssignableFrom(typeof(DateTime)) || pi.PropertyType.IsAssignableFrom(typeof(DateTime?)))
                        {
                            var newCastedValue = Convert.ToDateTime(newValue);
                            SetValueFromPath(item, newCastedValue, path, true);
                        }

                        return;
                    }

                    throw ex;

                }

            }
        }
    }


}
