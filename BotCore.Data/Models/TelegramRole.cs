using System;

namespace BotCore.Data.Models
{
    public class TelegramRole<TKey>
        where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; }
        public virtual string Name { get; set; }
    }
}
