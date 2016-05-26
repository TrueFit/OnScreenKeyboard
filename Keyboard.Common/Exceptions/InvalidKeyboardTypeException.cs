using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Keyboard.Common.Exceptions
{
    [Serializable]
    public class InvalidKeyboardTypeException : Exception
    {
        public InvalidKeyboardTypeException()
        {
        }
        
        public InvalidKeyboardTypeException(string message) : base(message)
        {
        }

        public InvalidKeyboardTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidKeyboardTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
