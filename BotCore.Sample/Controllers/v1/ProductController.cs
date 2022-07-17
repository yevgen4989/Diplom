using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BotCore.Sample.Models;
using Common;
using Data.Repositories;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFramework.Api;

namespace BotCore.Sample.Controllers.v1
{
    [ApiVersion("1")]
    public class ProductController : CrudUserController<ProductDto, ProductSelectDto, Product, int>
    {
        public ProductController(
            IRepository<Product> repository, IUserRepository userRepository, IMapper mapper
        )
            : base(repository, userRepository, mapper)
        {
        }
    }
}