﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotCore.Data.Models;

namespace BotCore.Data.Services
{
    public interface IRoleService<TKey, TRole>
        where TKey : IEquatable<TKey>
        where TRole : TelegramRole<TKey>
    {
        Task<bool> AnyRole(string role);
        Task<TRole> GetRole(string role);
        Task AddRole(TRole role);
        Task UpdateRole(TRole role);
        Task RemoveRole(TRole role);

        Task<bool> AnyUserRole(long telegramId, string role);
        Task AddUserRole(long telegramId, string role);
        Task RemoveUserRole(long telegramId, string role);

        Task<bool> AnyUserRole(string username, string role);
        Task AddUserRole(string username, string role);
        Task RemoveUserRole(string username, string role);

        Task<List<TRole>> GetUserRoles(long telegramId);
        Task<List<TRole>> GetUserRoles(string username);
    }
}
