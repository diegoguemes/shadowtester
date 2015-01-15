using System;
using System.Runtime.Serialization;

namespace ShadowTester.Domain.System
{
    public class ForegroundWindowNotFoundException : Exception
    {
        public ForegroundWindowNotFoundException()
        {
        }

        public ForegroundWindowNotFoundException(string message)
            : base(message)
        {
        }

        public ForegroundWindowNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ForegroundWindowNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}