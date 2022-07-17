using System.Collections.Generic;
using System.Threading;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BotCore.Sample.Models;
using Data.Repositories;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WebFramework.Api;
using WebFramework.FirebaseInfrastructure.Models;
using WebFramework.FirebaseInfrastructure.Services;

namespace BotCore.Sample.Controllers.v1
{
    [ApiVersion("1")]
    public class AuthController : BaseController
    {
        private readonly IFirebaseService _service;
        private IRepository<CategoryProduct> _repositoryCategoryProduct;
        private IRepository<Category> _repositoryCategory;
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public AuthController(
            IFirebaseService service, 
            IRepository<CategoryProduct> repositoryCategoryProduct, 
            IRepository<Category> repositoryCategory, 
            IUserRepository userRepository,
            IMapper mapper) : base()
        {
            _service = service;
            _repositoryCategoryProduct = repositoryCategoryProduct;
            _repositoryCategory = repositoryCategory;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        
        
        [AllowAnonymous]
        [HttpGet("SignInAnonymously")]
        public async Task<ActionResult<FirebaseUserToken>> SignInAnonymously()
        {
            return await _service.SignInAnonymously();
        }

        [AllowAnonymous]
        [HttpGet("SignUpWithEmailAndPassword")]
        public async Task<ActionResult<FirebaseUserToken>> SignUpWithEmailAndPassword(string email, string password)
        {
            return await _service.SignUpWithEmailAndPassword(email, password);
        }

        [AllowAnonymous]
        [HttpGet("SignInWithEmailAndPassword")]
        public async Task<ActionResult<FirebaseUserToken>> SignInWithEmailAndPassword(string email, string password)
        {
            return await _service.SignInWithEmailAndPassword(email, password);
        }

        [AllowAnonymous]
        [HttpGet("SignInWithGoogleAccessToken")]
        public async Task<ActionResult<FirebaseOAuthUserToken>> SignInWithGoogleAccessToken(string googleIdToken)
        {
            return await _service.SignInWithGoogleAccessToken(googleIdToken);
        }
        
        
        
        
        
        
        [AllowAnonymous]
        [HttpGet("RefreshToken")]
        public async Task<ActionResult<FirebaseRefreshToken>> RefreshToken(string refreshToken)
        {
            return await _service.RefreshToken(refreshToken);
        }
        
        [Authorize]
        [HttpGet("GetDataToken")]
        public async Task<ActionResult<FirebaseToken>> GetDataToken()
        {
            return await Task.FromResult((FirebaseToken)HttpContext.Items["user"]);
        }
        
        
        
        
        
        
        [Authorize]
        [HttpGet("GetUserOption")]
        public async Task<ActionResult<UserOptionSelectDto>> GetUserOption(CancellationToken cancellationToken)
        {
            string firebaseUserId = ((FirebaseToken) HttpContext.Items["user"]!).Claims["user_id"].ToString()!;
            
            var dto = await _userRepository.TableNoTracking.ProjectTo<UserOptionSelectDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.FirebaseId.Equals(firebaseUserId), cancellationToken);

            if (dto == null)
                return NotFound();

            return dto;
            
        }
        
        [Authorize]
        [HttpPut("UpdateUserOption")]
        public async Task<ApiResult<UserOptionSelectDto>> UpdateUserOption(UserOptionDto dto, CancellationToken cancellationToken)
        {
            string firebaseUserId = ((FirebaseToken) HttpContext.Items["user"]!).Claims["user_id"].ToString()!;
            var model = await _userRepository.GetByFirebaseIdAsync(firebaseUserId, cancellationToken);
            
            model = dto.ToEntity(_mapper, model);

            await _userRepository.UpdateAsync(model, cancellationToken);

            var resultDto = await _userRepository.TableNoTracking.ProjectTo<UserOptionSelectDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.FirebaseId == model.FirebaseId, cancellationToken);

            return resultDto;
        }
    }
}