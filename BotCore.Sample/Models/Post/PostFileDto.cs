#nullable enable

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Entities;
using WebFramework.Api;

namespace BotCore.Sample.Models {
    public class PostFileDto : BaseDto<PostFileDto, PostFile, int>
    {
        public string Path { get; set; }
        public string Type { get; set; }
        
        public int UserOptionId { get; set; }
        
        
        public ICollection<Post>? Posts { get; set; }
    }

    public class PostFileSelectDto : BaseDto<PostFileSelectDto, PostFile, int>
    {
        public string Path { get; set; }
        public string Type { get; set; }
        public int UserOptionId { get; set; }
        
        public ICollection<Post>? Posts { get; set; }
    }
}