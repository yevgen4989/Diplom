using System;
using System.Security.Cryptography;
using AutoMapper;
using Entities;
using WebFramework.Api;

namespace BotCore.Sample.Models
{
    public class PropertyValueDto : BaseDto<PropertyValueDto, PropertyValue, int>
    {
        public string Value { get; set; }
        public int PropertyId { get; set; }
        public int ProductId { get; set; }
    }
    
    public class PropertyValueSelectDto : BaseDto<PropertyValueSelectDto, PropertyValue, int>
    {
        public string Value { get; set; }
        public PropertySelectDto Property { get; set; }
        
        public override void CustomMappings(IMappingExpression<PropertyValue, PropertyValueSelectDto> mappingExpression)
        {
            mappingExpression
                .ForAllMembers(opts => 
                    opts.Condition((src, dest, srcMember) => srcMember != null)
                );
        }
    }
}