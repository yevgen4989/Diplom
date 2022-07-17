using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
using Google.Apis.Util;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Entities
{
    
    public class Post : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public PostStatus Status { get; set; } = PostStatus.New;
        public int? CategoryId { get; set; }
        public int UserOptionId { get; set; }
        public ICollection<PostFile>? PostFiles { get; set; }
        
        public PostPublishInfo PostPublishInfo { get; set; }
        public PostPublishSetting PostPublishSetting { get; set; }  

        public Category? Category { get; set; }
        
        [ForeignKey(nameof(UserOptionId))]
        public UserOption UserOption { get; set; }
    }
    
    public class PostPublishInfo
    {
        public long MessageId { get; set; }
        public string Link { get; set; }
        public DateTime PublishedTime { get; set; }
    }
    
    public class PostPublishSetting
    {
        public bool PublishCategory { get; set; } = false;
        
        public List<IndexItem> IndexItem { get; set; }
    }

    public class IndexItem
    {
        public int Index { get; set; }
        public int FileId { get; set; }
        public string AdditionalTags { get; set; }
        
        public bool PublishGeneralTags { get; set; }
        public bool PublishCategoryInPost { get; set; }
    }
    
    
    public enum PostStatus {
        New,
        Draft,
        Published,
        Deleted,
    }
    
    
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p => p.Title).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Description).IsRequired();

            builder.HasOne(p => p.Category).WithMany(c => c.Posts).HasForeignKey(p => p.CategoryId);
            builder.HasOne(p => p.UserOption).WithMany(c => c.Posts).HasForeignKey(p => p.UserOptionId);

            builder.HasMany(p => p.PostFiles).WithMany(c => c.Posts);

            builder.Property(e => e.PostPublishSetting).HasConversion(
                v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore}),
                v => JsonConvert.DeserializeObject<PostPublishSetting>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })!
            );
            
            builder.Property(e => e.PostPublishInfo).HasConversion(
                v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                v => JsonConvert.DeserializeObject<PostPublishInfo>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })!
            );
            
            builder.Property(e => e.Status).HasConversion(new EnumToStringConverter<PostStatus>());
            
        }
    }
}
