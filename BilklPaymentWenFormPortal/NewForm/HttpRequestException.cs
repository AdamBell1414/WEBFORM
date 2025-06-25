using System;
using System.Runtime.Serialization;

namespace BilklPaymentWenFormPortal.NewForm
{
    [Serializable]
    internal class HttpRequestException : Exception
    {
        public HttpRequestException()
        {
        }

        public HttpRequestException(string message) : base(message)
        {
        }

        public HttpRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected HttpRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}