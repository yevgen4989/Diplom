using System;

namespace Common.Exceptions
{
    public class LogicException : AppException
    {
        public LogicException() 
            : base(ApiResultStatusCode.LogicError)
        {
        }

        public LogicException(string Message) 
            : base(ApiResultStatusCode.LogicError, Message)
        {
        }

        public LogicException(object additionalData) 
            : base(ApiResultStatusCode.LogicError, additionalData)
        {
        }

        public LogicException(string Message, object additionalData) 
            : base(ApiResultStatusCode.LogicError, Message, additionalData)
        {
        }

        public LogicException(string Message, Exception exception)
            : base(ApiResultStatusCode.LogicError, Message, exception)
        {
        }

        public LogicException(string Message, Exception exception, object additionalData)
            : base(ApiResultStatusCode.LogicError, Message, exception, additionalData)
        {
        }
    }
}
