using System.Diagnostics.CodeAnalysis;
using BotCore.Data.Models;

namespace Entities.Bot
{
    public class ApplicationTgBotUser : TelegramBotUser<int>, IEntity<int>
    {
        [AllowNull]
        public override string? State { get; set; }
    }
}