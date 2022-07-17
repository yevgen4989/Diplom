#nullable enable
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Entities.Bot;
using WebFramework.Api;

namespace BotCore.Sample.Models.Bot
{
    public class ApplicationTgUserDto : BaseDto<ApplicationTgUserDto, ApplicationTgUser, int>
    {
        public long TelegramId { get; set; }
        public string Username { get; set; } = "";
        
        [AllowNull]
        public string? FirstName { get; set; }
        
        [AllowNull]
        public string? LastName { get; set; }

        public string LanguageCode { get; set; } = "";
    }

    public class ApplicationTgUserSelectDto : BaseDto<ApplicationTgUserSelectDto, ApplicationTgUser, int>
    {
        public long TelegramId { get; set; }
        public string Username { get; set; } = "";
        
        [AllowNull]
        public string? FirstName { get; set; }
        
        [AllowNull]
        public string? LastName { get; set; }
        
        public string LanguageCode { get; set; } = "";

        public ICollection<UserFileSelectDto> UserFiles { get; set; } = null!;

        public override void CustomMappings(IMappingExpression<ApplicationTgUser, ApplicationTgUserSelectDto> mappingExpression)
        {
            mappingExpression
                .ForAllMembers(opts => 
                    opts.Condition((src, dest, srcMember) => srcMember != null)
                );
            
            
        }
    }
}