using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using BotCore.Controllers;
using BotCore.Extensions.Collections;
using BotCore.Extensions.Collections.Items;
using BotCore.Managers;
using BotCore.Models;
using Telegram.Bot;

namespace BotCore.Extensions
{
    public static class ClientsExtensions
    {
        public static IServiceCollection AddTelegramClients(this IServiceCollection services, IEnumerable<BotData> data)
        {
            services.AddSingleton((servs) =>
                new ClientsCollection
                {
                    Clients = data.Select(
                        d => new ClientItem
                        {
                            Client = new TelegramBotClient(d.Token), BotData = d
                        }
                    ).ToList()
                });

            
            
            return services;
        }

        public static IServiceCollection AddTelegramManager(this IServiceCollection services)
        {
            services.AddSingleton((servs) =>
                new ControllersCollection
                {
                    ControllerTypes = AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(s => s.GetTypes())
                            .Where(p =>
                            {
                                return p.IsSubclassOf(typeof(CommandController));
                            }).ToList()
                });

            services.AddHostedService<TelegramManager>();

            return services;
        }
    }
}
