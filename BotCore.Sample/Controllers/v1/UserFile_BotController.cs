using AutoMapper;
using BotCore.Sample.Models.Bot;
using Data.Repositories;
using Entities.Bot;
using Microsoft.AspNetCore.Mvc;
using WebFramework.Api;

namespace BotCore.Sample.Controllers.v1
{
    [ApiVersion("1")]
    public class UserFile_BotController : CrudController<UserFileDto, UserFileSelectDto, UserFile, int>
    {
        public UserFile_BotController(IRepository<UserFile> repository, IMapper mapper) 
            : base(repository, mapper)
        {
        }
    }
}