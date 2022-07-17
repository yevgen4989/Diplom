﻿using Telegram.Bot;
using Telegram.Bot.Types;

namespace BotCore.Models
{
    public class CallbackQueryData
    {
        public ITelegramBotClient Client { get; set; }
        public CallbackQuery CallbackQuery { get; set; }
        public BotData BotData { get; set; }
    }
}
