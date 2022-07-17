using AutoMapper;
using Entities.Bot;
using WebFramework.Api;

namespace BotCore.Sample.Models.Bot
{
    public class UserFileDto : BaseDto<UserFileDto, UserFile>
    {
        public int TelegramUserId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }

    public class UserFileSelectDto : BaseDto<UserFileSelectDto, UserFile, int>
    {
        public int TelegramUserId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        
        public override void CustomMappings(IMappingExpression<UserFile, UserFileSelectDto> mappingExpression)
        {
            
        }
    }
}