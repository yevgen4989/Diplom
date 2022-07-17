using BotCore.Models;
using Telegram.Bot;

namespace BotCore.Extensions.Collections.Items
{
    public class ClientItem
    {
        public ITelegramBotClient Client { get; set; }
        public BotData BotData { get; set; }
    }
}
