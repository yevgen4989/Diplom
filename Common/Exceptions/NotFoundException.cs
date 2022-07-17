using System;

namespace Common.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException()
            : base(ApiResultStatusCode.NotFound, System.Net.HttpStatusCode.NotFound)
        {
        }

        public NotFoundException(string Message)
            : base(ApiResultStatusCode.NotFound, Message, System.Net.HttpStatusCode.NotFound)
        {
        }

        public NotFoundException(object additionalData)
            : base(ApiResultStatusCode.NotFound, null, System.Net.HttpStatusCode.NotFound, additionalData)
        {
        }

        public NotFoundException(string Message, object additionalData)
            : base(ApiResultStatusCode.NotFound, Message, System.Net.HttpStatusCode.NotFound, additionalData)
        {
        }

        public NotFoundException(string Message, Exception exception)
            : base(ApiResultStatusCode.NotFound, Message, exception, System.Net.HttpStatusCode.NotFound)
        {
        }

        public NotFoundException(string Message, Exception exception, object additionalData)
            : base(ApiResultStatusCode.NotFound, Message, System.Net.HttpStatusCode.NotFound, exception, additionalData)
        {
        }
    }
}
