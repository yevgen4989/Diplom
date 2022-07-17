using System;
using AutoMapper;
using Entities;
using WebFramework.Api;

namespace BotCore.Sample.Models
{
    public class DeliveryDto : BaseDto<DeliveryDto, Delivery, int>
    {
        public string Name { get; set; }
        public bool Active { get; set; }
    }
    
    public class DeliverySelectDto : BaseDto<DeliverySelectDto, Delivery, int>
    {
        public string Name { get; set; }
        public bool Active { get; set; }

        public override void CustomMappings(IMappingExpression<Delivery, DeliverySelectDto> mappingExpression)
        {
            mappingExpression
                .ForAllMembers(opts => 
                    opts.Condition((src, dest, srcMember) => srcMember != null)
                );
        }
    }
}