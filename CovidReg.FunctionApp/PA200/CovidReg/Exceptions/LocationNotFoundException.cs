using System;
using System.Runtime.Serialization;

namespace CovidReg.FunctionApp.PA200.CovidReg.Exceptions
{
    public class LocationNotFoundException : Exception
    {
        public LocationNotFoundException()
        {
        }

        protected LocationNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public LocationNotFoundException(string? message) : base(message)
        {
        }

        public LocationNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}