using System;
using AutoMapper;
using Entities;
using WebFramework.Api;

namespace BotCore.Sample.Models
{
    public class TelegramChannelDto : BaseDto<TelegramChannelDto, TelegramChannel, int>
    {
        public string Name { get; set; }
        public long ChannelId { get; set; }
        public bool Active { get; set; }
        public int UserOptionId { get; set; }
    }
    
    public class TelegramChannelSelectDto : BaseDto<TelegramChannelSelectDto, TelegramChannel, int>
    {

        public string Name { get; set; }
        public long ChannelId { get; set; }
        public bool Active { get; set; }
        public int UserOptionId { get; set; }
        
        public UserOption UserOption { get; set; }


        public override void CustomMappings(IMappingExpression<TelegramChannel, TelegramChannelSelectDto> mappingExpression)
        {
            mappingExpression
                .ForAllMembers(opts => 
                    opts.Condition((src, dest, srcMember) => srcMember != null)
                );
        }
    }
}