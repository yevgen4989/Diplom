using System.Threading.Tasks;
using BotCore.Attributes;
using BotCore.Data.Attributes;
using BotCore.Data.Controllers;
using BotCore.Data.Factories;
using BotCore.Data.Services;
using BotCore.Models;
using Entities.Bot;
using Telegram.Bot;

namespace BotCore.Sample.BotControllers
{
    [BotName("test1_diplom_bot")]
    // TextController contains one more good example!
    public class StateController : CommandController<int>
    {
        private IUserService<int, ApplicationTgUser> _userService;

        public override void Initialize(IServiceFactory factory, long telegramId)
        {
            _userService = factory.CreateUserService<int, ApplicationTgUser>(BotId);
        }

        [Command("get_state")]
        public async Task GetState(MessageData messageData)
        {
            await messageData.Client.SendTextMessageAsync(messageData.Message.Chat.Id,
                await _userService.GetState(messageData.Message.Chat.Id));
        }

        [Command("set_state_test1")]
        public async Task SetTest1State(MessageData messageData)
        {
            await _userService.SetState(messageData.Message.Chat.Id, "Test1State");
            await messageData.Client.SendTextMessageAsync(messageData.Message.Chat.Id, "Test1 state setted!");
        }

        [Command("set_state_test2")]
        public async Task SetTest2State(MessageData messageData)
        {
            await _userService.SetState(messageData.Message.Chat.Id, "Test2State");
            await messageData.Client.SendTextMessageAsync(messageData.Message.Chat.Id, "Test2 state setted!");
        }


        [Command("set_state_test3")]
        public async Task SetTest3State(MessageData messageData)
        {
            await _userService.SetState(messageData.Message.Chat.Id, "Test3State");
            await messageData.Client.SendTextMessageAsync(messageData.Message.Chat.Id, "Test3 state setted!");
        }

        [State("Test1State")]
        [Command("check_state_test1")]
        public async Task CheckTest1State(MessageData messageData)
        {
            await messageData.Client.SendTextMessageAsync(messageData.Message.Chat.Id, "This method allowed for you! (Test1State)");
        }

        [State("Test2State")]
        [Command("check_state_test2")]
        public async Task CheckTest2State(MessageData messageData)
        {
            await messageData.Client.SendTextMessageAsync(messageData.Message.Chat.Id, "This method allowed for you! (Test2State)");
        }

        [State("Test1State")]
        [State("Test2State")]
        [Command("check_state_test12")]
        public async Task CheckTest12State(MessageData messageData)
        {
            await messageData.Client.SendTextMessageAsync(messageData.Message.Chat.Id, "This method allowed for you!");
        }
    }
}
