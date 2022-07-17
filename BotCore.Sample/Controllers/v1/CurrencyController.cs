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
    public class CurrencyController : CrudController<CurrencyDto, CurrencySelectDto, Currency, int>
    {
        public CurrencyController(IRepository<Currency> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}