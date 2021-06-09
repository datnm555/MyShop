using System;
using System.Runtime.Serialization;

namespace MyShop.DataAccess.InMemory.ExtentionException
{
    [Serializable]
    internal class NotFound : Exception
    {
        public NotFound()
        {
        }

        public NotFound(string message) : base(message)
        {
        }

        public NotFound(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected NotFound(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}