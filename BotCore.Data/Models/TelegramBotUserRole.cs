using System;

namespace BotCore.Data.Models
{
    public class TelegramBotUserRole<TKey>
        where TKey : IEquatable<TKey>
    {
        public virtual TKey TelegramBotUserId { get; set; }
        public virtual TKey TelegramRoleId { get; set; }
    }
}
