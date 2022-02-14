using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;

namespace Authenticador.Domain.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class HttpException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }

        public HttpException() { }

        public HttpException(string message, Exception innerException) : base(message, innerException) { }

        protected HttpException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public HttpException(HttpStatusCode statusCode, string mensagem) : base(mensagem)
        {
            StatusCode = statusCode;
        }
    }
}
