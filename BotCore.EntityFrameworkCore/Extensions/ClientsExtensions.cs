using BotCore.Data.Controllers;
using BotCore.Data.Helpers;
using BotCore.EntityFrameworkCore.Managers;
using BotCore.EntityFrameworkCore.Options;
using BotCore.Extensions.Collections;
using BotCore.Managers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BotCore.EntityFrameworkCore.Extensions
{
    public static class ClientsExtensions
    {
        public static IServiceCollection AddTelegramDbManager(this IServiceCollection services)
        {
            services.AddSingleton((servs) =>
                new ControllersCollection
                {
                    ControllerTypes = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(s => s.GetTypes())
                        .Where(p =>
                        {
                            return TypeHelper.IsTypeDerivedFromGenericType(p, typeof(CommandController<>));
                        }).ToList()
                });

            services.AddHostedService((IServiceProvider provider) =>
            {
                ContextOptions options = provider.GetService<ContextOptions>();

                return (TelegramManager)ActivatorUtilities.CreateInstance(provider,
                    typeof(TelegramDbManager<,,,,>).MakeGenericType(new Type[] {
                        options.KeyType, options.UserType, options.BotUserType, options.RoleType, options.BotType
                }));
            });

            return services;
        }
    }
}
