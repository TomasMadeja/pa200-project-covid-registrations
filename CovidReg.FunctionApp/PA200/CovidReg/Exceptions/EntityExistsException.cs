using System;
using System.Runtime.Serialization;

namespace CovidReg.FunctionApp.PA200.CovidReg.Exceptions
{
    public class EntityExistsException : Exception
    {
        public EntityExistsException()
        {
        }

        protected EntityExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public EntityExistsException(string? message) : base(message)
        {
        }

        public EntityExistsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}