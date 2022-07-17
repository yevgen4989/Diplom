﻿using BotCore.Data.Factories;
using BotCore.Data.Models;
using BotCore.EntityFrameworkCore.Factories;
using BotCore.EntityFrameworkCore.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BotCore.EntityFrameworkCore.Extensions
{
    public static class StoreExtensions
    {
        private static Func<Type, bool> GetSetTypeCheck(Type checkedType)
        {
            return t =>
            {
                Type baseType = t.BaseType;

                return baseType != null && baseType.IsGenericType
                    && baseType.GetGenericTypeDefinition() == checkedType;
            };
        }

        public static IServiceCollection AddTelegramStore<TContext>(this IServiceCollection services)
            where TContext : class
        {
            Type contextType = typeof(TContext);

            Type dbSet = typeof(DbSet<>);
            List<Type> setTypes = contextType.GetProperties()
                .Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == dbSet)
                .Select(p => p.PropertyType.GenericTypeArguments[0]).ToList();

            Type userType = typeof(TelegramUser<>);
            Type roleType = typeof(TelegramRole<>);
            Type botType = typeof(TelegramBot<>);
            Type botUserType = typeof(TelegramBotUser<>);
            Type botUserRoleType = typeof(TelegramBotUserRole<>);

            ContextOptions options = new()
            {
                ContextType = contextType,

                UserType = setTypes.FirstOrDefault(GetSetTypeCheck(userType)),
                RoleType = setTypes.FirstOrDefault(GetSetTypeCheck(roleType)),
                BotType = setTypes.FirstOrDefault(GetSetTypeCheck(botType)),
                BotUserType = setTypes.FirstOrDefault(GetSetTypeCheck(botUserType)),
                BotUserRoleType = setTypes.FirstOrDefault(GetSetTypeCheck(botUserRoleType))
            };

            options.KeyType = options.UserType.BaseType.GenericTypeArguments[0];

            services.AddSingleton(options);
            services.AddTransient<IServiceFactory, ServiceFactory>();

            return services;
        }
    }
}
