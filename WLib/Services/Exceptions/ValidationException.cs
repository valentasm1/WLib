using System;
using System.Collections.Generic;
using System.Text;

namespace WLib.Core.Services.Exceptions
{
    public class ValidationException : Exception
    {
        public override string Message { get; }

        public ValidationException()
        {

        }

        public ValidationException(string key, string error)
        {

        }


        public virtual ValidationException AddError(string key, string error)
        {
            throw new NotImplementedException();
            return this;
        }

        public virtual IEnumerator<KeyValuePair<string, IList<string>>> GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
