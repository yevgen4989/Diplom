﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using BotCore.Extensions.Collections;
using BotCore.Extensions.Collections.Items;
using BotCore.Handlers;
using BotCore.Models;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;

namespace BotCore.Managers
{
    public class TelegramManager : BackgroundService
    {
        protected readonly ControllersCollection _controllersCollection;
        protected readonly ILogger<TelegramManager> _logger;

        public TelegramManager(IServiceProvider services,
            ControllersCollection controllersCollection,
            ILoggerFactory loggerFactory)
        {
            Services = services;

            _controllersCollection = controllersCollection;
            _logger = loggerFactory.CreateLogger<TelegramManager>();
        }

        protected IServiceProvider Services { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await DoWork(stoppingToken);
        }

        private MessageHandler GetMessageHandler(ITelegramBotClient client, BotData botData)
        {
            return new MessageHandler(_controllersCollection, client, botData, Services);
        }

        protected virtual async Task DoWork(CancellationToken stoppingToken)
        {
            ClientsCollection clientsCollection = Services.GetService<ClientsCollection>();

            foreach (ClientItem client in clientsCollection.Clients)
            {
                MessageHandler messageHandler = GetMessageHandler(client.Client, client.BotData);

                ReceiverOptions receiverOptions = new() { AllowedUpdates = { } };
                
                client.Client.StartReceiving(
                    async (client, update, token) => await messageHandler.OnUpdate(client, update, token),
                    (client, exception, token) => _logger.LogError(exception.ToString()),
                    receiverOptions, 
                    cancellationToken: stoppingToken
                );
            }

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}
