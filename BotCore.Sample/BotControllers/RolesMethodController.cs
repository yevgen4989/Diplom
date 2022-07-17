using System.Threading.Tasks;
using BotCore.Attributes;
using BotCore.Data.Attributes;
using BotCore.Data.Controllers;
using BotCore.Models;
using Telegram.Bot;

namespace BotCore.Sample.BotControllers
{
    [BotName("test1_diplom_bot")]
    public class RolesMethodController : CommandController<int>
    {
        [Role("admin")]
        [Command("admin_role_method")]
        public async Task RoleControllerMethod(MessageData data)
        {
            await data.Client.SendTextMessageAsync(data.Message.Chat.Id, $"You have permission for this command in method!");
        }

        [Role("manager")]
        [Command("manager_role_method")]
        public async Task AnotherRoleControllerMethod(MessageData data)
        {
            await data.Client.SendTextMessageAsync(data.Message.Chat.Id, $"You have permission for this command in method!");
        }

        [Role("admin")]
        [Role("manager")]
        [Command("union_role_method")]
        public async Task UnionRoleControllerMethod(MessageData data)
        {
            await data.Client.SendTextMessageAsync(data.Message.Chat.Id, $"You have permission for this command in method!");
        }

        [Command("no_admin_role_method")]
        public async Task NoRoleControllerMethod(MessageData data)
        {
            await data.Client.SendTextMessageAsync(data.Message.Chat.Id, $"You have permission for this command in method!");
        }
    }
}
