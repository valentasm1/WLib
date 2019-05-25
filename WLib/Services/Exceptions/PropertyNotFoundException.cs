using System;
using System.Collections.Generic;
using System.Text;

namespace WLib.Core.Services.Exceptions
{
    public class PropertyNotFoundException : Exception
    {
        public PropertyNotFoundException(string message) :
            base(message)
        {
        }

        public PropertyNotFoundException(string message,
            Exception innerException) :
            base(message, innerException)
        {
        }

        public PropertyNotFoundException(Type type, string property,
            Exception innerException) :
            this(string.Format("Property '{0}' was not found in type {1}", property, type),
                innerException)
        {
        }

        public PropertyNotFoundException(Type type, string property) :
            base(string.Format("Property '{0}' was not found in type {1}", property, type))
        {
        }
    }
}
