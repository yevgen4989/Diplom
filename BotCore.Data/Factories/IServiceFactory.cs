using System;
using BotCore.Data.Models;
using BotCore.Data.Services;

namespace BotCore.Data.Factories;

public interface IServiceFactory
{
    IUserService<TKey, TUser> CreateUserService<TKey, TUser>(TKey botId)
        where TKey : IEquatable<TKey>
        where TUser : TelegramUser<TKey>;

    IRoleService<TKey, TRole> CreateRoleService<TKey, TRole>(TKey botId)
        where TKey : IEquatable<TKey>
        where TRole : TelegramRole<TKey>;
}