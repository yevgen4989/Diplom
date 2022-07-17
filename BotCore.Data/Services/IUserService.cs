﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotCore.Data.Models;
using Telegram.Bot.Types;

namespace BotCore.Data.Services
{
    public interface IUserService<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : TelegramUser<TKey>
    {
        Task CheckUser(User user);

        Task<List<TUser>> GetUsers();
        Task<int> CountUsers();

        Task<bool> AnyUser(string username);
        Task<bool> AnyUser(long telegramId);

        Task<TUser> GetUser(string username);
        Task<TUser> GetUser(long telegramId);

        Task<TKey> GetBotUserId(long telegramId);
        Task<TKey> GetBotUserId(string username);

        Task<List<TUser>> GetUsersByRole(string role);

        Task SetState(long telegramId, string value);
        Task SetState(string username, string value);

        Task<string> GetState(long telegramId);
        Task<string> GetState(string username);

        Task BlockBot(long telegramId);
        Task BlockBot(string username);
    }
}
