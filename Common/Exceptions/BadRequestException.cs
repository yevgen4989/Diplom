using System;

namespace Common.Exceptions
{
    public class BadRequestException : AppException
    {
        public BadRequestException()
            : base(ApiResultStatusCode.BadRequest, System.Net.HttpStatusCode.BadRequest)
        {
        }

        public BadRequestException(string Message)
            : base(ApiResultStatusCode.BadRequest, Message, System.Net.HttpStatusCode.BadRequest)
        {
        }

        public BadRequestException(object additionalData)
            : base(ApiResultStatusCode.BadRequest, null, System.Net.HttpStatusCode.BadRequest, additionalData)
        {
        }

        public BadRequestException(string Message, object additionalData)
            : base(ApiResultStatusCode.BadRequest, Message, System.Net.HttpStatusCode.BadRequest, additionalData)
        {
        }

        public BadRequestException(string Message, Exception exception)
            : base(ApiResultStatusCode.BadRequest, Message, exception, System.Net.HttpStatusCode.BadRequest)
        {
        }

        public BadRequestException(string Message, Exception exception, object additionalData)
            : base(ApiResultStatusCode.BadRequest, Message, System.Net.HttpStatusCode.BadRequest, exception, additionalData)
        {
        }
    }
}
