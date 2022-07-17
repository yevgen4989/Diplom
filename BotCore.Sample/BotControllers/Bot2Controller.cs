using System;
using System.Threading.Tasks;
using BotCore.Attributes;
using BotCore.Data.Controllers;
using BotCore.Data.Factories;
using BotCore.Data.Services;
using BotCore.Models;
using Entities.Bot;
using Telegram.Bot;

namespace BotCore.Sample.BotControllers
{
    [BotName("test2_diplom_bot")]
    public class Sample2Controller : CommandController<int>
    {
        private IRoleService<int, ApplicationTgRole> _roleService;

        public override void Initialize(IServiceFactory factory, long telegramId)
        {
            _roleService = factory.CreateRoleService<int, ApplicationTgRole>(BotId);
        }

        [Command("start")]
        public async Task Start(MessageData data)
        {
            await data.Client.SendTextMessageAsync(data.Message.Chat.Id, $"You pressed: /start");
        }

        [Command("add_admin_role")]
        public async Task AddAdminRole(MessageData data)
        {
            if (!await _roleService.AnyRole("admin"))
            {
                await _roleService.AddRole(new ApplicationTgRole { Name = "admin" });
            }

            if (await _roleService.AnyUserRole(data.Message.Chat.Id, "admin"))
            {
                await data.Client.SendTextMessageAsync(data.Message.Chat.Id, $"You already have admin role!");
            }
            else
            {
                await _roleService.AddUserRole(data.Message.Chat.Id, "admin");
                await data.Client.SendTextMessageAsync(data.Message.Chat.Id, $"You add admin role!");
            }
        }

        [Command("remove_admin_role")]
        public async Task RemoveAdminRole(MessageData data)
        {
            if (await _roleService.AnyUserRole(data.Message.Chat.Id, "admin"))
                await _roleService.RemoveUserRole(data.Message.Chat.Id, "admin");

            await data.Client.SendTextMessageAsync(data.Message.Chat.Id, $"You remove admin role!");
        }

        [DefaultCommand]
        public async Task DefaultCommand(MessageData data)
        {
            await data.Client.SendTextMessageAsync(data.Message.Chat.Id, $"You pressed unknown command: {data.Message.Text}");
        }

        [Command("exception")]
        public Task Exception(MessageData data)
        {
            throw new NotImplementedException();
        }
    }
}