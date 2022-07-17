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
    [Role("admin")]
    [BotName("test1_diplom_bot")]
    public class RolesController : CommandController<int>
    {
        
        private IRoleService<int, ApplicationTgRole> _roleService;
        private IUserService<int, ApplicationTgUser> _userService;
        
        public override void Initialize(IServiceFactory factory, long telegramId)
        {
            _roleService = factory.CreateRoleService<int, ApplicationTgRole>(BotId);
            _userService = factory.CreateUserService<int, ApplicationTgUser>(BotId);
        }
        
        
        [Command("admin_role_controller")]
        public async Task RoleControllerMethod(MessageData data)
        {
            await data.Client.SendTextMessageAsync(data.Message.Chat.Id, $"You have permission for this command in controller!");
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
    }
}
