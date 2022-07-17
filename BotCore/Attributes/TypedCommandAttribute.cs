using System;
using Telegram.Bot.Types.Enums;

namespace BotCore.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class TypedCommandAttribute : Attribute
    {
        private readonly MessageType _type;

        public TypedCommandAttribute(MessageType type)
        {
            _type = type;
        }

        public MessageType GetMessageType()
        {
            return _type;
        }
    }
}
