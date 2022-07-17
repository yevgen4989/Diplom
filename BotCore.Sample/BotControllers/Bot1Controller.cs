using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BotCore.Attributes;
using BotCore.Data.Attributes;
using BotCore.Data.Controllers;
using BotCore.Data.Factories;
using BotCore.Data.Services;
using BotCore.Models;
using BotCore.Sample.Models;
using Common.Utilities;
using Data.Repositories;
using Entities;
using Entities.Bot;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BotCore.Sample.BotControllers
{
    [BotName("test1_diplom_bot")]
    public class SampleController : CommandController<int>
    {
        private IRoleService<int, ApplicationTgRole> _roleService;
        private IUserService<int, ApplicationTgUser> _userService;

        private readonly IRepository<TelegramChannel> _repositoryTelegramChannel;
        protected readonly IUserRepository _userRepository;
        
        public SampleController(IRepository<TelegramChannel> repositoryTelegramChannel, IUserRepository userRepository) {
            _repositoryTelegramChannel = repositoryTelegramChannel;
            _userRepository = userRepository;
        }

        public override void Initialize(IServiceFactory factory, long telegramId)
        {
            _roleService = factory.CreateRoleService<int, ApplicationTgRole>(BotId);
            _userService = factory.CreateUserService<int, ApplicationTgUser>(BotId);
        }


        [Command("start")]
        public async Task Start(MessageData data)
        {
            await _userService.SetState(data.Message.Chat.Id, "Init");

            if (!await _roleService.AnyRole("admin")) {
                await _roleService.AddRole(new ApplicationTgRole { Name = "admin" });   
            }

            await data.Client.SendTextMessageAsync(data.Message.Chat.Id, $"You pressed: /start");
        }
        
        [State("Init")]
        [Command("connect_channel")]
        public async Task ConnectChannel(MessageData data)
        {
            await _userService.SetState(data.Message.Chat.Id, "ChannelBinding");
            await data.Client.SendTextMessageAsync(data.Message.Chat.Id, $"You pressed: /connect_channel\n\nПришлите ссылку на ваш канал");
        }

        [TextCommand]
        [State("ChannelBinding")]
        public async Task TestState(MessageData data)
        {
            try
            {
                Uri uri = StringExtensions.ToUri(data.Message.Text);
                if (uri.Host != "t.me" && uri.Host != "www.t.me" && StringExtensions.CountByCharacter(uri.AbsolutePath, '/') != 1) {
                    await data.Client.SendTextMessageAsync(data.Message.Chat.Id, $"Это не ссылка на ваш канал.\n\nПроверьте ссылку ещё раз и отправьте её снова");
                }
                else 
                {
                    bool checkAdminAccess = false;
                    String channelUsername = uri.AbsolutePath.Replace("/", "");
                    String me = data.Message.From!.Username;
                    ChatMember[] admins = await data.Client.GetChatAdministratorsAsync("@" + channelUsername);

                    UserOption userOption = _userRepository.TableNoTracking
                        .Where(x => x.UsernameTelegram == me)
                        .OrderBy(x => x.Id)
                        .ToList()
                        .FirstOrDefault();

                    if (_repositoryTelegramChannel.TableNoTracking.Where(x => x.Name == channelUsername).ToList().Count >= 1) {
                        await data.Client.SendTextMessageAsync(data.Message.Chat.Id, $"Канал уже подключён к системе и привязан к пользователю!");
                    }
                    else {
                        foreach (var chatMember in admins) {
                            if (chatMember.User.Username  == me){
                                checkAdminAccess = true;
                                break;
                            }
                        }

                        if (checkAdminAccess) {
                            Message msg = await data.Client.SendTextMessageAsync("@"+channelUsername, $"Канал успешно подключён к системе!");

                            if (userOption != null) {

                                TelegramChannel channel = new TelegramChannel
                                {
                                    Name = msg.Chat.Username!,
                                    ChannelId = msg.Chat.Id,
                                    UserOptionId = userOption.Id,
                                    Active = true
                                };

                                await _repositoryTelegramChannel.AddAsync(channel, CancellationToken.None);
                                await _roleService.AddUserRole(data.Message.Chat.Id, "admin");
                                await _userService.SetState(data.Message.Chat.Id, "Init");
                            }

                        }
                        else
                        {
                            await data.Client.SendTextMessageAsync(data.Message.Chat.Id, $"Вы не администратор этого канала!");
                        }   
                    }

                }
            }
            catch (Exception e) {
                await data.Client.SendTextMessageAsync(data.Message.Chat.Id, $"{e.Message}\n\nПожалуйста повторите попытку ещё раз");    
            }
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        [DefaultCommand]
        public async Task DefaultCommand(MessageData data)
        {
            await data.Client.SendTextMessageAsync(data.Message.Chat.Id, $"You pressed unknown command: {data.Message.Text}");
        }

        [Command("exception")]
        public Task Exception(MessageData data)
        {
            throw new NotImplementedException();
        }
    }
}
