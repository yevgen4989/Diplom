using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities
{
    public class PostFile : BaseEntity
    {
        public string Path { get; set; }
        public string Type { get; set; }
        
        public int UserOptionId { get; set; }
        
        [ForeignKey(nameof(UserOptionId))]
        public UserOption UserOption { get; set; }
        
        public ICollection<Post>? Posts { get; set; }
    }
    
    public class PostImageConfiguration : IEntityTypeConfiguration<PostFile>
    {
        public void Configure(EntityTypeBuilder<PostFile> builder)
        {
            builder.HasMany(p => p.Posts).WithMany(c => c.PostFiles);
            
            builder.HasOne(p => p.UserOption).WithMany(c => c.PostFiles).HasForeignKey(p => p.UserOptionId);
        }
    }
}