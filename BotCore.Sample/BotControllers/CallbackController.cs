using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotCore.Attributes;
using BotCore.Data.Controllers;
using BotCore.Models;
using BotCore.Sample.Models;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotCore.Sample.BotControllers
{
    [BotName("test1_diplom_bot")]
    public class CallbackController : CommandController<int>
    {
        [Command("query")]
        public async Task CallbackQuery(MessageData data)
        {
            await data.Client.SendTextMessageAsync(
                chatId: data.Message.Chat.Id,
                text: $"Callback query",
                replyMarkup: new InlineKeyboardMarkup(
                    new List<List<InlineKeyboardButton>>
                    {
                        new List<InlineKeyboardButton>
                        {
                            new InlineKeyboardButton("True")
                            {
                                CallbackData = JsonConvert.SerializeObject(
                                    new TestCallbackQueryModel
                                    {
                                        Path = "test",
                                        SomeData = true
                                    })
                            },
                            new InlineKeyboardButton("False")
                            {
                                CallbackData = JsonConvert.SerializeObject(
                                    new TestCallbackQueryModel
                                    {
                                        Path = "test",
                                        SomeData = false
                                    })
                            },
                            new InlineKeyboardButton("Default")
                            {
                                CallbackData = JsonConvert.SerializeObject(
                                    new CallbackQueryModel {
                                        Path = "default"
                                    })
                            }
                        }
                    }
                )
            );
        }

        [CallbackQuery("test")]
        public async Task CallbackQuery(CallbackQueryData data, TestCallbackQueryModel model)
        {
            await data.Client.SendTextMessageAsync(data.CallbackQuery.Message.Chat.Id, $"Model: {model.SomeData}");
        }

        [CallbackDefaultQuery]
        public async Task CallbackDefaultQuery(CallbackQueryData data, CallbackQueryModel model)
        {
            await data.Client.SendTextMessageAsync(data.CallbackQuery.Message.Chat.Id, $"Callback Default Query");
        }
    }
}
