using System;
using System.Runtime.Serialization;

namespace DataAccess.Data.Services
{
    [Serializable]
    public class ServerSideException : Exception
    {
        public ServerSideException()
        {
        }

        public ServerSideException(string message) : base(message)
        {
        }

        public ServerSideException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ServerSideException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class InvalidDataProvidedException : Exception
    {
        public InvalidDataProvidedException()
        {
        }

        public InvalidDataProvidedException(string message) : base(message)
        {
        }

        public InvalidDataProvidedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidDataProvidedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}