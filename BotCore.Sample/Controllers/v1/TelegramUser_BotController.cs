using AutoMapper;
using BotCore.Sample.Models.Bot;
using Data.Repositories;
using Entities.Bot;
using Microsoft.AspNetCore.Mvc;
using WebFramework.Api;

namespace BotCore.Sample.Controllers.v1
{
    [ApiVersion("1")]
    public class TelegramUser_Bot : CrudController<ApplicationTgUserDto, ApplicationTgUserSelectDto, ApplicationTgUser, int>
    {
        public TelegramUser_Bot(IRepository<ApplicationTgUser> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}