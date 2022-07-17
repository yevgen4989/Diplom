using System.Threading.Tasks;
using BotCore.Attributes;
using BotCore.Data.Controllers;
using BotCore.Models;
using Telegram.Bot;

namespace BotCore.Sample.BotControllers
{
    public class InlineController : CommandController<int>
    {
        [InlineQuery]
        public async Task InlineSample(InlineQueryData data)
        {
            await data.Client.SendTextMessageAsync(
                data.InlineQuery.From.Id, $"You enter: {data.InlineQuery.Query}");
        }
    }
}
