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
    public class PaymentController : CrudController<PaymentDto, PaymentSelectDto, Payment, int>
    {
        public PaymentController(IRepository<Payment> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }   
    }
}