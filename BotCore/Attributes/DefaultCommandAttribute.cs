using System;

namespace BotCore.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class DefaultCommandAttribute : Attribute
    {
    }
}
