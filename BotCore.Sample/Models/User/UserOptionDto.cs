#nullable enable

using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using WebFramework.Api;

namespace BotCore.Sample.Models
{
    public class UserOptionDto : BaseDto<UserOptionDto, UserOption, int>
    {
        public string Email { get; set; }
        public string FirebaseId { get; set; }
        public string DisplayName { get; set; }
        public DateTimeOffset? LastLoginDate { get; set; }
        public string? UsernameTelegram { get; set; }

    }
    
    public class UserOptionSelectDto : BaseDto<UserOptionSelectDto, UserOption, int>
    {
        public string Email { get; set; }
        public string FirebaseId { get; set; }
        public string DisplayName { get; set; }
        public DateTimeOffset? LastLoginDate { get; set; }
        
        public string? UsernameTelegram { get; set; }

        public ICollection<PostSelectDto> Posts { get; set; }
        public ICollection<CategorySelectDto> Categories { get; set; }
        
        public ICollection<TelegramChannelSelectDto> Channels { get; set; }
        

        public override void CustomMappings(IMappingExpression<UserOption, UserOptionSelectDto> mappingExpression)
        {
            mappingExpression
                .ForAllMembers(opts => 
                    opts.Condition((src, dest, srcMember) => srcMember != null)
                );
        }
    }
}
