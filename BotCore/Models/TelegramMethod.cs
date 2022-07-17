using System;
using System.Reflection;

namespace BotCore.Models
{
    public class TelegramMethod
    {
        public Type ControllerType { get; set; }
        public MethodInfo Method { get; set; }
    }
}
