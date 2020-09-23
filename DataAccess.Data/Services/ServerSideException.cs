using System;
using System.Runtime.Serialization;

namespace DataAccess.Data.Services
{
    [Serializable]
    internal class ServerSideException : Exception
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
}