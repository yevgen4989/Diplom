using BotCore.Data.Factories;
using BotCore.Data.Models;
using BotCore.Data.Services;
using BotCore.EntityFrameworkCore.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BotCore.EntityFrameworkCore.Factories
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly IServiceProvider _provider;

        public ServiceFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public IUserService<TKey, TUser> CreateUserService<TKey, TUser>(TKey botId)
            where TKey : IEquatable<TKey>
            where TUser : TelegramUser<TKey>
        {
            return (IUserService<TKey, TUser>)ActivatorUtilities.CreateInstance(_provider, typeof(UserService<TKey, TUser>), botId);
        }

        public IRoleService<TKey, TRole> CreateRoleService<TKey, TRole>(TKey botId)
           where TKey : IEquatable<TKey>
           where TRole : TelegramRole<TKey>
        {
            return (IRoleService<TKey, TRole>)ActivatorUtilities.CreateInstance(_provider, typeof(RoleService<TKey, TRole>), botId);
        }
    }
}
