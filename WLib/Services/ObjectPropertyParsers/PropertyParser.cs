using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using WLib.Core.Services.SystemServices;

namespace WLib.Core.Services.ObjectPropertyParsers
{
    public class PropertyParser
    {
        private readonly char _seperator = '.';
        public PropertyParser(char seperator)
        {
            this._seperator = seperator;
        }
        public static PropertyParser ClassParser = new PropertyParser('.');

        /// <summary>
        /// Returns the part from the string
        /// </summary>
        /// <example>if path = Student.Group.Name, return Student </example>
        /// <returns></returns>
        public string GetFirstPart(string path)
        {
            string[] parts = path.Split(_seperator);
            return parts[0];
        }

        /// <summary>
        /// Returns the tail from the string
        /// </summary>
        /// <example> ifpath = Student.Group.Name, return Group.Name </example>
        /// <returns></returns>
        public string GetTailPart(string path)
        {
            int index = path.IndexOf(_seperator);
            if (index < 0)
                return string.Empty;
            index++;
            return path.Substring(index);
        }

        public string GetBasePath(string path)
        {
            int index = path.LastIndexOf(_seperator);
            if (index < 0)
                return String.Empty;
            return path.Substring(0, index);
        }
        /// <summary>
        /// Strips the base path from the propertyPath
        /// </summary>
        /// <example>Person.Family.Children.Name would return Name</example>
        /// <example>Name would return name</example>
        public string StripBasePath(string path)
        {
            int index = path.LastIndexOf(_seperator);
            if (index < 0)
                return path;
            int length = path.Length;
            return path.Substring(index + 1, length - index - 1);
        }



        /// <summary>
        /// Tells if the given property is path of properties, or a single property
        /// </summary>
        /// <example>Person.Family.Children.Name return true</example>
        /// <example>Person.Name return true</example>
        /// <example>Name would return false</example>
        public bool ContainsPath(string propertyPath)
        {
            int index = propertyPath.LastIndexOf(_seperator);
            if (index < 0)
                return false;
            return true;
        }

        public static bool IsNullable(System.Type type)
        {
            System.Type nil = Type.GetType("System.Nullable`1");
            if (type.IsGenericType && type.FullName.StartsWith(nil.FullName))
                return true;
            return false;

        }

        /// <summary>
        /// If given type is nullabe, return underlygin type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static System.Type StripNullabe(System.Type type)
        {
            if (IsNullable(type) == false)
                return type;
            Type[] args = type.GetGenericArguments();
            // if length is greater than 1
            // we have something odd here, surely not nullabe
            // return original type, so the program continue to run
            // normally
            if (args.Length > 1)
                return type;
            return args[0];
        }
    }
}
