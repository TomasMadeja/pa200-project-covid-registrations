using System;
using System.Runtime.Serialization;

namespace CovidReg.FunctionApp.PA200.CovidReg.Exceptions
{
    public class AlreadyRegisteredException : Exception
    {
        public AlreadyRegisteredException()
        {
        }

        protected AlreadyRegisteredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public AlreadyRegisteredException(string? message) : base(message)
        {
        }

        public AlreadyRegisteredException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}