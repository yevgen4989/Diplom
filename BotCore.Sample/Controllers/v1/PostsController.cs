using System;
using AutoMapper;
using Data.Repositories;
using Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using BotCore.Extensions.Collections;
using BotCore.Extensions.Collections.Items;
using BotCore.Handlers;
using BotCore.Sample.Models;
using Entities.Bot;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using WebFramework.Api;

namespace BotCore.Sample.Controllers.v1
{
    [ApiVersion("1")]
    public class PostsController : CrudUserController<PostDto, PostSelectDto, Post, int>
    {
        private readonly ITelegramBotClient _client;
        protected readonly IServiceProvider _provider;
        
        
        private readonly IRepository<PostFile> _postFileRepository;
        private readonly IRepository<ApplicationTgUser> _tgUserRepository;
        private readonly IRepository<TelegramChannel> _telegramChannelRepository;

        public PostsController(
            
            IRepository<PostFile> postImageRepository,
            IRepository<ApplicationTgUser> tgUserRepository,
            IRepository<TelegramChannel> telegramChannelRepository,
            
            IServiceProvider provider,
            
            IRepository<Post> repository,
            IUserRepository userRepository,
            IMapper mapper
        ) : base(repository, userRepository, mapper)
        {
            _provider = provider;
            
            ClientsCollection clientsCollection = _provider.GetService<ClientsCollection>();
            if (clientsCollection != null)
            {
                foreach (ClientItem client in clientsCollection.Clients)
                {
                    if (client.BotData.Name == "test1_diplom_bot")
                    {
                        _client = client.Client;
                    }
                }   
            }

            _telegramChannelRepository = telegramChannelRepository;
            _postFileRepository = postImageRepository;
            _tgUserRepository = tgUserRepository;
        }

        public override Task<ApiResult<PostSelectDto>> Create(PostDto dto, CancellationToken cancellationToken)
        {
            dto.Status = PostStatus.New;
            
            return base.Create(dto, cancellationToken);
        }

        public override Task<ApiResult<PostSelectDto>> Update(int id, PostDto dto, CancellationToken cancellationToken)
        {
            dto.Status = PostStatus.Draft;
            
            return base.Update(id, dto, cancellationToken);
        }

        [HttpPost("[action]")]
        public async Task<ApiResult<PostSelectDto>> MoveToTrash(int id, CancellationToken cancellationToken)
        {
            var model = await Repository.GetByIdAsync(cancellationToken, id);

            model.Status = PostStatus.Deleted;

            await Repository.UpdateAsync(model, cancellationToken);
            
            var resultDto = await Repository.TableNoTracking.ProjectTo<PostSelectDto>(Mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Id.Equals(model.Id), cancellationToken);
            
            return resultDto;

        }


        [HttpGet("[action]")]
        public async Task<ApiResult<PostSelectDto>> AddFileToPost(int idPost, int idImage, CancellationToken cancellationToken)
        {
            string firebaseUserId = ((FirebaseToken) HttpContext.Items["user"]!).Claims["user_id"].ToString()!;
            UserOption userOption = await UserRepository.GetByFirebaseIdAsync(firebaseUserId, cancellationToken);
            var userId = userOption!.Id;
            
            var modelPost = await Repository.GetByIdAsync(cancellationToken, idPost);
            var modelFile = await _postFileRepository.GetByIdAsync(cancellationToken, idImage);
            
            if (modelPost.UserOptionId != userId) {
                return NotFound();
            }

            modelPost.PostFiles ??= new List<PostFile>();
            modelPost.PostFiles.Add(modelFile);
            
            await Repository.UpdateAsync(modelPost, cancellationToken);
            
            var resultDto = await Repository.TableNoTracking.ProjectTo<PostSelectDto>(Mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Id.Equals(modelPost.Id), cancellationToken);
            
            return resultDto;
        }
        
        [HttpGet("[action]")]
        public async Task<ApiResult<PostSelectDto>> Publish(int id, string toChannel, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(toChannel)) {
                return NotFound();
            }

            string firebaseUserId = ((FirebaseToken) HttpContext.Items["user"]!).Claims["user_id"].ToString()!;
            UserOption userOption = await UserRepository.GetByFirebaseIdAsync(firebaseUserId, cancellationToken);
            var userId = userOption!.Id;
            
            var model = await Repository.GetByIdAsync(cancellationToken, id);
            var channel = await _telegramChannelRepository.TableNoTracking.FirstOrDefaultAsync(x => x.Name == toChannel, cancellationToken: cancellationToken);
            
            ApplicationTgUser tgUser = await _tgUserRepository.TableNoTracking.FirstOrDefaultAsync(x =>
                x.Username == userOption.UsernameTelegram, 
                cancellationToken: cancellationToken
            );
            
            if (channel == null || tgUser == null) {
                return NotFound();
            }
            
            // Message msg =  await _client.SendTextMessageAsync(
            //     channel.ChannelId, 
            //     "Test send message #1", 
            //     ParseMode.Html,
            //     cancellationToken: cancellationToken
            // );

            List<string> images = new List<string>
            {
                "https://cdn.pixabay.com/photo/2017/06/20/19/22/fuchs-2424369_640.jpg",
                "https://cdn.pixabay.com/photo/2017/04/11/21/34/giraffe-2222908_640.jpg"
            };
            List<IAlbumInputMedia> media = new List<IAlbumInputMedia>();

            for (int i = 0; i < images.Count; i++)
            {
                if (i == 0)
                {
                    FileStream stream = System.IO.File.OpenRead(images[i]);

                    media.Add(new InputMediaPhoto(
                        new InputMedia(stream, stream.Name)) 
                        {
                            Caption = "fffffffffffffff",
                            ParseMode = ParseMode.Html
                        }
                    ); 
                }
                else
                {
                    media.Add(
                        new InputMediaPhoto(images[i])
                    );
                }
            }

            Message[] messages = await _client.SendMediaGroupAsync(
                chatId: channel.ChannelId,
                media: media,
                cancellationToken: cancellationToken
            );
            
            
            
            
            
            
            
            //TODO публикация товара: если у товара только 1 медиа-объект то публиковать через SendPhoto с кнопками; если у товара несколько медиа-объектов, то публиковать в 2 сообщения: 1 - сообщение альбома через SendMediaGroup и 2 - сообщение с детальным описанием товара и кнопками 
            

            // var modelPublish = new PostPublishInfo();
            // modelPublish.PostId = model.Id;
            // modelPublish.PublishedTime = DateTime.Now;
            // modelPublish.Link = "";
            //
            // model.Published = true;
            //
            //
            // await Repository.UpdateAsync(model, cancellationToken);

            //return NotFound();
            
            var resultDto = await Repository.TableNoTracking.ProjectTo<PostSelectDto>(Mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Id.Equals(model.Id), cancellationToken);
            
            return resultDto;
        }
    }
}
