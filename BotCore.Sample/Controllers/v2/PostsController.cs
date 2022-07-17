using AutoMapper;
using Data.Repositories;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BotCore.Sample.Models;
using WebFramework.Api;

// namespace BotCore.Sample.Controllers.v2
// {
//     [ApiVersion("2")]
//     public class PostsController : v1.PostsController
//     {
//         public PostsController(IRepository<Post> repository, IUserRepository userRepository, IMapper mapper)
//             : base(repository, userRepository, mapper)
//         {
//         }
//
//         public override Task<ApiResult<PostDto>> Create(PostDto dto, CancellationToken cancellationToken)
//         {
//             return base.Create(dto, cancellationToken);
//         }
//
//         [NonAction]
//         public override Task<ApiResult> Delete(Guid id, CancellationToken cancellationToken)
//         {
//             return base.Delete(id, cancellationToken);
//         }
//
//         public async override Task<ActionResult<List<PostSelectDto>>> Get(CancellationToken cancellationToken)
//         {
//             return await Task.FromResult(new List<PostSelectDto>
//             {
//                 new PostSelectDto
//                 {
//                      FullTitle = "FullTitle",
//                      // UserFullName =  "UserOptionFullName",
//                      CategoryName = "CategoryName",
//                      Description = "Description",
//                      Title = "Title",
//                 }
//             });
//         }
//
//         public async override Task<ApiResult<PostSelectDto>> Get(Guid id, CancellationToken cancellationToken)
//         {
//             if (Guid.Empty == id)
//                 return NotFound();
//             return await base.Get(id, cancellationToken);
//         }
//
//         [HttpGet("Test")]
//         public ActionResult Test()
//         {
//             return Content("This is test");
//         }
//
//         public override Task<ApiResult<PostSelectDto>> Update(Guid id, PostDto dto, CancellationToken cancellationToken)
//         {
//             return base.Update(id, dto, cancellationToken);
//         }
//     }
// }
