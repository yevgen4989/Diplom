using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Repositories;
using Entities;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebFramework.Api
{
    [ApiVersion("1")]
    public class CrudUserController<TDto, TSelectDto, TEntity, TKey> : BaseController
        where TDto : BaseDto<TDto, TEntity, TKey>, new()
        where TSelectDto : BaseDto<TSelectDto, TEntity, TKey>, new()
        where TEntity : class, IEntity<TKey>, new()
    {
        protected readonly IMapper Mapper;
        protected readonly IRepository<TEntity> Repository;
        protected readonly IUserRepository UserRepository;

        public CrudUserController(IRepository<TEntity> repository, IUserRepository userRepository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
            UserRepository = userRepository;
        }
    
        [HttpGet]
        public virtual async Task<ActionResult<List<TSelectDto>>> Get(CancellationToken cancellationToken)
        {
            string firebaseUserId = ((FirebaseToken) HttpContext.Items["user"]!).Claims["user_id"].ToString()!;
            UserOption? userOption = await UserRepository.GetByFirebaseIdAsync(firebaseUserId, cancellationToken);
            var userId = userOption!.Id;

            var list = await Repository.TableNoTracking
                .ProjectTo<TSelectDto>(Mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            list = list.Where(x => {
                if (x.GetType().GetProperties().FirstOrDefault(y => y.Name == "UserOptionId")!.GetValue(x)!.Equals(userId)) {
                    return true;   
                }
                return false;
            }).ToList();

            return Ok(list);
        }
    
        [HttpGet("{id}")]
        public virtual async Task<ApiResult<TSelectDto>> Get(TKey id, CancellationToken cancellationToken)
        {
            string firebaseUserId = ((FirebaseToken) HttpContext.Items["user"]!).Claims["user_id"].ToString()!;
            UserOption? userOption = await UserRepository.GetByFirebaseIdAsync(firebaseUserId, cancellationToken);
            var userId = userOption!.Id;
            
            var dto = await Repository.TableNoTracking
                .ProjectTo<TSelectDto>(Mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Id.Equals(id), cancellationToken);
    
            if (dto == null || !(dto.GetType().GetProperties().FirstOrDefault(y => y.Name == "UserOptionId")!.GetValue(dto)!.Equals(userId)))
                return NotFound();
    
            return dto;
        }
    
        [HttpPost]
        public virtual async Task<ApiResult<TSelectDto>> Create(TDto dto, CancellationToken cancellationToken)
        {
            string firebaseUserId = ((FirebaseToken) HttpContext.Items["user"]!).Claims["user_id"].ToString()!;
            UserOption? userOption = await UserRepository.GetByFirebaseIdAsync(firebaseUserId, cancellationToken);
            var userId = userOption!.Id;


            var propertyUser = typeof(TDto).GetProperties().FirstOrDefault(p => p.Name == "UserOptionId");
            if (propertyUser != null)
            {
                PropertyInfo propertyInfo = typeof(TDto).GetProperty("UserOptionId");
                propertyInfo.SetValue(dto, Convert.ChangeType(userId, propertyInfo.PropertyType), null);
            }
            
            var model = dto.ToEntity(Mapper);

            await Repository.AddAsync(model, cancellationToken);
    
            var resultDto = await Repository.TableNoTracking
                .ProjectTo<TSelectDto>(Mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Id.Equals(model.Id), cancellationToken);
    
            return resultDto;
        }
    
        [HttpPut("{id}")]
        public virtual async Task<ApiResult<TSelectDto>> Update(TKey id, TDto dto, CancellationToken cancellationToken)
        {
            string firebaseUserId = ((FirebaseToken) HttpContext.Items["user"]!).Claims["user_id"].ToString()!;
            UserOption? userOption = await UserRepository.GetByFirebaseIdAsync(firebaseUserId, cancellationToken);
            var userId = userOption!.Id;

            var model = await Repository.GetByIdAsync(cancellationToken, id);

            if (!model.GetType().GetProperties().FirstOrDefault(y => y.Name == "UserOptionId")!.GetValue(model)!.Equals(userId))
            {
                return NotFound();
            }
    
            var propertyUser = typeof(TDto).GetProperties().FirstOrDefault(p => p.Name == "UserOptionId");
            if (propertyUser != null)
            {
                PropertyInfo propertyInfo = typeof(TDto).GetProperty("UserOptionId");
                propertyInfo.SetValue(dto, Convert.ChangeType(userId, propertyInfo.PropertyType), null);
            }

            var propertyId = typeof(TDto).GetProperties().FirstOrDefault(p => p.Name == "Id");
            if (propertyId != null)
            {
                PropertyInfo propertyInfo = typeof(TDto).GetProperty("Id");
                propertyInfo.SetValue(dto, Convert.ChangeType(id, propertyInfo.PropertyType), null);
            }

            model = dto.ToEntity(Mapper, model);
    
            await Repository.UpdateAsync(model, cancellationToken);
    
            var resultDto = await Repository.TableNoTracking.ProjectTo<TSelectDto>(Mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Id.Equals(model.Id), cancellationToken);
    
            return resultDto;
        }
    
        [HttpDelete("{id}")]
        public virtual async Task<ApiResult> Delete(TKey id, CancellationToken cancellationToken)
        {
            var model = await Repository.GetByIdAsync(cancellationToken, id);
    
            string firebaseUserId = ((FirebaseToken) HttpContext.Items["user"]!).Claims["user_id"].ToString()!;
            UserOption? userOption = await UserRepository.GetByFirebaseIdAsync(firebaseUserId, cancellationToken);
            var userId = userOption!.Id;
            
            if (model.GetType().GetProperties().FirstOrDefault(y => y.Name == "UserOptionId")!.GetValue(model)!.Equals(userId))
            {
                await Repository.DeleteAsync(model, cancellationToken);
                return Ok();
            }
    
            return NotFound();
        }
    }
    
    public class CrudUserController<TDto, TSelectDto, TEntity> : CrudUserController<TDto, TSelectDto, TEntity, int>
        where TDto : BaseDto<TDto, TEntity, int>, new()
        where TSelectDto : BaseDto<TSelectDto, TEntity, int>, new()
        where TEntity : class, IEntity<int>, new()
    {
        public CrudUserController(IRepository<TEntity> repository, IUserRepository userRepository, IMapper mapper)
            : base(repository, userRepository, mapper)
        {
        }
    }
    
    public class CrudUserController<TDto, TEntity> : CrudUserController<TDto, TDto, TEntity, int>
        where TDto : BaseDto<TDto, TEntity, int>, new()
        where TEntity : class, IEntity<int>, new()
    {
        public CrudUserController(IRepository<TEntity> repository, IUserRepository userRepository, IMapper mapper)
            : base(repository, userRepository, mapper)
        {
        }
    }  
};