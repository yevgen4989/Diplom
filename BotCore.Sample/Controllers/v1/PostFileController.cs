using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BotCore.Sample.Models;
using Common.Utilities;
using Data.Repositories;
using Entities;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebFramework.Api;

namespace BotCore.Sample.Controllers.v1
{
    [ApiVersion("1")]
    public class PostFileController : CrudUserController<PostFileDto, PostFileSelectDto, PostFile, int>
    {
        private static IWebHostEnvironment _webHostEnvironment;

        public PostFileController(
            IRepository<PostFile> repository,
            IUserRepository userRepository,
            IMapper mapper,
            
            IWebHostEnvironment webHostEnvironment
            
        ) : base(repository, userRepository, mapper)
        {
            _webHostEnvironment = webHostEnvironment;
        }



        [HttpPost("[action]")]
        public async Task<ApiResult<PostFileSelectDto>> UploadFile([FromForm]IFormFile file, CancellationToken cancellationToken)
        {

            string firebaseUserId = ((FirebaseToken) HttpContext.Items["user"]!).Claims["user_id"].ToString()!;
            UserOption userOption = await UserRepository.GetByFirebaseIdAsync(firebaseUserId, cancellationToken);
            var userId = userOption!.Id;
            
            
            string fileUrl = "";
            string folderRoot = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            
            if (!Directory.Exists(folderRoot)) {
                Directory.CreateDirectory(folderRoot);
            }
                
            if (file.Length > 0) {
                string filePath = Guid.NewGuid() + "-" + file.FileName;
                using (var stream = new FileStream(Path.Combine(folderRoot, filePath), FileMode.Create)) {
                    file.CopyTo(stream);
                    stream.Flush();
                }
                fileUrl = "/uploads/" + filePath;
            }

            PostFileDto postFileDto = new PostFileDto
            {
                Path = fileUrl,
                Type = new FileTypeDetect(file.FileName.Split('.').Last()).GetFileTypeName(),
            };

            return await base.Create(postFileDto, cancellationToken);
        }
    }
}