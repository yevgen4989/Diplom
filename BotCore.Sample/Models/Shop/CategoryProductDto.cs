using System;
using AutoMapper;
using Entities;
using WebFramework.Api;

namespace BotCore.Sample.Models
{
    public class CategoryProductDto : BaseDto<CategoryProductDto, CategoryProduct, int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserOptionId { get; set; }
    }
    
    public class CategoryProductSelectDto : BaseDto<CategoryProductSelectDto, CategoryProduct, int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserOptionId { get; set; }
        
        public override void CustomMappings(IMappingExpression<CategoryProduct, CategoryProductSelectDto> mappingExpression)
        {
            mappingExpression
                .ForAllMembers(opts => 
                    opts.Condition((src, dest, srcMember) => srcMember != null)
                );
        }
    }
}