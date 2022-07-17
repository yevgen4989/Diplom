using System;
using Common;
using Entities;
using AutoMapper;
using System.Threading;
using WebFramework.Api;
using Data.Repositories;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BotCore.Sample.Models;
using Microsoft.AspNetCore.Authorization;

namespace BotCore.Sample.Controllers.v1
{
    [ApiVersion("1")]
    public class CategoryProductController : CrudUserController<CategoryProductDto, CategoryProductSelectDto, CategoryProduct, int>
    {
        public IRepository<Product> RepositoryProduct;

        public CategoryProductController(
            IRepository<CategoryProduct> repository, 
            IUserRepository userRepository,
            IMapper mapper,
            IRepository<Product> repositoryProduct) 
            : base(repository, userRepository, mapper)
        {
            RepositoryProduct = repositoryProduct;
        }
        
        [HttpPost("{action}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult<List<ModelProductCategory>>> AddProductToCategory([FromBody]IEnumerable<ModelProductCategory> data, [FromHeader]CancellationToken cancellationToken)
        {
            // CancellationTokenSource cts = new CancellationTokenSource();
            // CancellationToken token = cts.Token;
            
            var userId = HttpContext.User.Identity.GetUserId<int>();
            if (data == null) {
                return NotFound();
            }
            List<ModelProductCategory> categoryProducts = new List<ModelProductCategory>();
            foreach (var modelProductCategory in data)
            {
                
                var categoryProduct = await Repository.GetByIdAsync(cancellationToken, modelProductCategory.CategoryId);
                var product = await RepositoryProduct.GetByIdAsync(cancellationToken, modelProductCategory.ProductId);
                
                // await Repository.UpdateAsync(categoryProduct, cancellationToken);

                if (product == null)
                {
                    return BadRequest("Product "+modelProductCategory.ProductId.ToString()+" not found");
                }
                else if (product.UserOption.Id != userId)
                {
                    return BadRequest("Product "+modelProductCategory.ProductId.ToString()+" access denied!");
                }
                
                if (categoryProduct == null)
                {
                    return BadRequest("Category "+modelProductCategory.CategoryId.ToString()+" not found");
                }
                else if (categoryProduct.UserOptionId != userId)
                {
                    return BadRequest("Category "+modelProductCategory.ProductId.ToString()+" access denied!");
                }
                
                
                categoryProduct.Products.Add(product);
                await Repository.UpdateAsync(categoryProduct, cancellationToken);

                // modelProductCategory.Result = true;
                // categoryProducts.Add(await Repository.GetByIdAsync(cancellationToken, categoryProduct.Id));

            }
            
            
            return Ok(data);
        }
    }
}