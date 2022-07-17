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
    public class OrderController : CrudUserController<OrderDto, OrderSelectDto, Order, int>
    {
        public OrderController(
            IRepository<Order> repository, IUserRepository userRepository, IMapper mapper)
            : base(repository, userRepository, mapper)
        {
        }   
    }
}