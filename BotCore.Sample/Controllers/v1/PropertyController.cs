using System;
using AutoMapper;
using BotCore.Sample.Models;
using Data.Repositories;
using Entities;
using Microsoft.AspNetCore.Mvc;
using WebFramework.Api;

namespace BotCore.Sample.Controllers.v1
{
    [ApiVersion("1")]
    public class PropertyController : CrudUserController<PropertyDto, PropertySelectDto, Property, int>
    {
        public PropertyController(
            IRepository<Property> repository, IUserRepository userRepository, IMapper mapper
        )
            : base(repository, userRepository, mapper)
        {
        }   
    }
}