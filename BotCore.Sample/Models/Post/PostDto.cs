#nullable enable
using AutoMapper;
using Entities;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using WebFramework.Api;

namespace BotCore.Sample.Models
{
    public class PostDto : BaseDto<PostDto, Post, int>
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public PostStatus Status { get; set; }
        
        public int? CategoryId { get; set; }
        public int UserOptionId { get; set; }
        
        public ICollection<PostFile>? PostFiles { get; set; }
        
        public PostPublishInfo PostPublishInfo { get; set; }
        public PostPublishSetting PostPublishSetting { get; set; }  

        public Category? Category { get; set; }




    }

    public class PostSelectDto : BaseDto<PostSelectDto, Post, int>
    {
        // public string UserFullName { get; set; } //=> User.FullName

        public string Title { get; set; }
        public string Description { get; set; }

        public PostStatus Status { get; set; }
        
        public int? CategoryId { get; set; }
        public CategorySelectDto Category { get; set; }
        public string? FullTitle { get; set; } // => mapped from "Title (Category.Name)"
        public string? CategoryName { get; set; } //=> Category.Name
        
        public int UserOptionId { get; set; }
        
        public ICollection<PostFile>? PostFiles { get; set; }
        
        public PostPublishInfo PostPublishInfo { get; set; }
        public PostPublishSetting PostPublishSetting { get; set; }

        public override void CustomMappings(IMappingExpression<Post, PostSelectDto> mappingExpression)
        {
            mappingExpression.ForMember(
                dest => dest.FullTitle,
                config => 
                    config.MapFrom(
                        src => 
                            src.Category != null ? 
                                $"{src.Title} ({src.Category.Name})" : $"{src.Title}"
                    )
            );
        }
    }
}
