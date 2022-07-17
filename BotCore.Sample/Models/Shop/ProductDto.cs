#nullable enable
using System;
using System.Collections.Generic;
using AutoMapper;
using Entities;
using WebFramework.Api;
using WebFramework.CustomMapping;

namespace BotCore.Sample.Models
{
    public class ProductDto : BaseDto<ProductDto, Product, int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TypeProduct Type { get; set; }
        public bool IsOffer { get; set; }
        public int UserOptionId { get; set; }
        public int ParentProductId { get; set; }
    }

    public class ProductSelectDto : BaseDto<ProductSelectDto, Product, int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TypeProduct Type { get; set; }
        public bool IsOffer { get; set; }
        public int UserOptionId { get; set; }
        
        public int ParentProductId { get; set; }

        public ICollection<ProductSelectDto>? ChildProducts { get; set; }
        public ICollection<CategoryProductSelectDto>? Categories { get; set; }
        public ICollection<PropertyValueSelectDto>? PropertiesValues { get; set; }
        
        public override void CustomMappings(IMappingExpression<Product, ProductSelectDto> mappingExpression)
        {
            mappingExpression
                .ForAllMembers(opts => 
                        opts.Condition((src, dest, srcMember) => srcMember != null)
                );
            // mappingExpression.

            // mappingExpression.ForMember(
            //     dest => dest.FullTitle,
            //     config => config.MapFrom(src => $"{src.Title} ({src.Category.Name})"));
        }
    }
}