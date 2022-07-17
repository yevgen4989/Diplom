using System;
using System.Net;

namespace Common.Exceptions
{
    public class AppException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public ApiResultStatusCode ApiStatusCode { get; set; }
        public object AdditionalData { get; set; }

        public AppException()
            : this(ApiResultStatusCode.ServerError)
        {
        }

        public AppException(ApiResultStatusCode StatusCode)
            : this(StatusCode, null)
        {
        }

        public AppException(string Message)
            : this(ApiResultStatusCode.ServerError, Message)
        {
        }

        public AppException(ApiResultStatusCode StatusCode, string Message)
            : this(StatusCode, Message, HttpStatusCode.InternalServerError)
        {
        }

        public AppException(string Message, object additionalData)
            : this(ApiResultStatusCode.ServerError, Message, additionalData)
        {
        }

        public AppException(ApiResultStatusCode StatusCode, object additionalData)
            : this(StatusCode, null, additionalData)
        {
        }

        public AppException(ApiResultStatusCode StatusCode, string Message, object additionalData)
            : this(StatusCode, Message, HttpStatusCode.InternalServerError, additionalData)
        {
        }

        public AppException(ApiResultStatusCode StatusCode, string Message, HttpStatusCode httpStatusCode)
            : this(StatusCode, Message, httpStatusCode, null)
        {
        }

        public AppException(ApiResultStatusCode StatusCode, string Message, HttpStatusCode httpStatusCode, object additionalData)
            : this(StatusCode, Message, httpStatusCode, null, additionalData)
        {
        }

        public AppException(string Message, Exception exception)
            : this(ApiResultStatusCode.ServerError, Message, exception)
        {
        }

        public AppException(string Message, Exception exception, object additionalData)
            : this(ApiResultStatusCode.ServerError, Message, exception, additionalData)
        {
        }

        public AppException(ApiResultStatusCode StatusCode, string Message, Exception exception)
            : this(StatusCode, Message, HttpStatusCode.InternalServerError, exception)
        {
        }

        public AppException(ApiResultStatusCode StatusCode, string Message, Exception exception, object additionalData)
            : this(StatusCode, Message, HttpStatusCode.InternalServerError, exception, additionalData)
        {
        }

        public AppException(ApiResultStatusCode StatusCode, string Message, HttpStatusCode httpStatusCode, Exception exception)
            : this(StatusCode, Message, httpStatusCode, exception, null)
        {
        }

        public AppException(ApiResultStatusCode StatusCode, string Message, HttpStatusCode httpStatusCode, Exception exception, object additionalData) : base(Message, exception)
        {
            ApiStatusCode = StatusCode;
            HttpStatusCode = httpStatusCode;
            AdditionalData = additionalData;
        }
    }
}
