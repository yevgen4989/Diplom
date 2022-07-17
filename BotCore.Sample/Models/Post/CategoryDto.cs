#nullable enable

using System;
using System.Collections.Generic;
using AutoMapper;
using Entities;
using WebFramework.Api;

namespace BotCore.Sample.Models
{
    public class CategoryDto : BaseDto<CategoryDto, Category, int>
    {
        public string Name { get; set; } = "";
        public int? ParentCategoryId { get; set; }
        public int UserOptionId { get; set; }

    }
    
    public class CategorySelectDto : BaseDto<CategorySelectDto, Category, int>
    {
        public string Name { get; set; } = "";
        
        public int? ParentCategoryId { get; set; }
        public string? ParentCategoryName { get; set; } //=> mapped from ParentCategory.Name
        
        public int UserOptionId { get; set; }

        public ICollection<CategorySelectDto>? ChildCategories { get; set; }
        public ICollection<PostSelectDto>? Posts { get; set; }
        
        public override void CustomMappings(IMappingExpression<Category, CategorySelectDto> mappingExpression)
        {
            
        }
    }
}
