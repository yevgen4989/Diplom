using AutoMapper;
using BotCore.Sample.Models.Bot;
using Data.Repositories;
using Entities.Bot;
using Microsoft.AspNetCore.Mvc;
using WebFramework.Api;

namespace BotCore.Sample.Controllers.v1
{
    [ApiVersion("1")]
    public class TelegramRole_BotController : CrudController<ApplicationTgRoleDto, ApplicationTgRoleSelectDto, ApplicationTgRole, int>
    {
        public TelegramRole_BotController(IRepository<ApplicationTgRole> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}