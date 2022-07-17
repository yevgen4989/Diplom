using BotCore.Data.Models;

namespace Entities.Bot
{
    public class ApplicationTgBotUserRole : TelegramBotUserRole<int>, IEntity<int>
    {
        public int Id { get; set; }
    }
}