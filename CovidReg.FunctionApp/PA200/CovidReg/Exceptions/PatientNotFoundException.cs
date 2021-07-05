using System;
using System.Runtime.Serialization;

namespace CovidReg.FunctionApp.PA200.CovidReg.Exceptions
{
    public class PatientNotFoundException : Exception
    {
        public PatientNotFoundException()
        {
        }

        protected PatientNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public PatientNotFoundException(string? message) : base(message)
        {
        }

        public PatientNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}