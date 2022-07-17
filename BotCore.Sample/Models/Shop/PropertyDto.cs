using System;
using AutoMapper;
using Entities;
using WebFramework.Api;

namespace BotCore.Sample.Models
{
    public class PropertyDto : BaseDto<PropertyDto, Property, int>
    {
        public string Name { get; set; }
        public TypeProperty Type { get; set; }
        public bool ForOffer { get; set; }
        public bool ForCategory { get; set; }
        public int UserOptionId { get; set; }
    }
    
    public class PropertySelectDto : BaseDto<PropertySelectDto, Property, int>
    {
        public string Name { get; set; }
        public TypeProperty Type { get; set; }
        public bool ForOffer { get; set; }
        public bool ForCategory { get; set; }
        public int UserOptionId { get; set; }

        public override void CustomMappings(IMappingExpression<Property, PropertySelectDto> mappingExpression)
        {
            mappingExpression
                .ForAllMembers(opts => 
                    opts.Condition((src, dest, srcMember) => srcMember != null)
                );
            
            /*mappingExpression.ForMember(dest => dest.)*/
        }
    }
}