using AutoMapper;
using BotCore.Sample.Models.Bot;
using Data.Repositories;
using Entities.Bot;
using Microsoft.AspNetCore.Mvc;
using WebFramework.Api;

namespace BotCore.Sample.Controllers.v1
{
    [ApiVersion("1")]
    public class TelegramBotUser_BotController : CrudController<ApplicationTgBotUserDto, ApplicationTgBotUserSelectDto, ApplicationTgBotUser, int>
    {
        public TelegramBotUser_BotController(IRepository<ApplicationTgBotUser> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}