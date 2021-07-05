using System;
using System.Runtime.Serialization;

namespace CovidReg.FunctionApp.PA200.CovidReg.Exceptions
{
    public class FullSlotException : Exception
    {
        public FullSlotException()
        {
        }

        protected FullSlotException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public FullSlotException(string? message) : base(message)
        {
        }

        public FullSlotException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}