using System;
using System.Threading.Tasks;
using BotCore.Attributes;
using BotCore.Data.Controllers;
using BotCore.Models;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace BotCore.Sample.BotControllers
{
    [BotName("test1_diplom_bot")]
    public class TypedController : CommandController<int>
    {
        [TypedCommand(MessageType.Photo)]
        public async Task Photo(MessageData data)
        {
            await data.Client.SendPhotoAsync(data.Message.Chat.Id, data.Message.Photo[0].FileId);
        }

        [TypedCommand(MessageType.Video)]
        public async Task Video(MessageData data)
        {
            await data.Client.SendVideoAsync(data.Message.Chat.Id, data.Message.Video.FileId);
        }

        [TypedCommand(MessageType.Audio)]
        public async Task Audio(MessageData data)
        {
            await data.Client.SendAudioAsync(data.Message.Chat.Id, data.Message.Audio.FileId);
        }

        [TypedCommand(MessageType.ChatMembersAdded)]
        public Task ChatMembersAdded(MessageData data)
        {
            throw new NotImplementedException();
        }
        
        [TypedCommand(MessageType.ChatMemberLeft)]
        public Task ChatMemberLeft(MessageData data)
        {
            throw new NotImplementedException();
        }
    }
}
