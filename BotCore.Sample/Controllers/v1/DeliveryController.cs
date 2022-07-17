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
    public class DeliveryController : CrudController<DeliveryDto, DeliverySelectDto, Delivery, int>
    {
        public DeliveryController(IRepository<Delivery> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }   
    }
}