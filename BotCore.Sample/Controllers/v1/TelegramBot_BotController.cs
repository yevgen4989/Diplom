using AutoMapper;
using BotCore.Sample.Models.Bot;
using Data.Repositories;
using Entities.Bot;
using Microsoft.AspNetCore.Mvc;
using WebFramework.Api;

namespace BotCore.Sample.Controllers.v1
{
    [ApiVersion("1")]
    public class TelegramBot_BotController : CrudController<ApplicationTgBotDto, ApplicationTgBotSelectDto, ApplicationTgBot, int>
    {
        public TelegramBot_BotController(IRepository<ApplicationTgBot> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}