using System;
using System.Diagnostics.CodeAnalysis;

namespace BotCore.Data.Models
{
    public class TelegramUser<TKey>
        where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; }

        public virtual long TelegramId { get; set; }
        public virtual string Username { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string LanguageCode { get; set; }
    }
}
