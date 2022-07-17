using AutoMapper;
using BotCore.Sample.Models.Bot;
using Data.Repositories;
using Entities.Bot;
using Microsoft.AspNetCore.Mvc;
using WebFramework.Api;

namespace BotCore.Sample.Controllers.v1
{
    [ApiVersion("1")]
    public class TelegramBotUserRole_BotController : CrudController<ApplicationTgBotUserRoleDto, ApplicationTgBotUserRoleSelectDto, ApplicationTgBotUserRole, int>
    {
        public TelegramBotUserRole_BotController(IRepository<ApplicationTgBotUserRole> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}