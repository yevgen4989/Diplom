#nullable enable
using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Entities.Bot;
using WebFramework.Api;

namespace BotCore.Sample.Models.Bot
{
    public class ApplicationTgBotUserDto : BaseDto<ApplicationTgBotUserDto, ApplicationTgBotUser, int>
    {
        public int TelegramUserId { get; set; }
        public int TelegramBotId { get; set; }

        public bool BotBlocked { get; set; }
        
        [AllowNull]
        public string? State { get; set; }
    }

    public class ApplicationTgBotUserSelectDto : BaseDto<ApplicationTgBotUserSelectDto, ApplicationTgBotUser, int>
    {
        public int TelegramUserId { get; set; }
        public int TelegramBotId { get; set; }

        public bool BotBlocked { get; set; }
        
        [AllowNull]
        public string? State { get; set; }
        
        public override void CustomMappings(IMappingExpression<ApplicationTgBotUser, ApplicationTgBotUserSelectDto> mappingExpression)
        {
            mappingExpression
                .ForAllMembers(opts => 
                    opts.Condition((src, dest, srcMember) => srcMember != null)
                );
        }
    }
}