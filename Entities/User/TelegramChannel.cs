using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities
{
    public class TelegramChannel : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public long ChannelId { get; set; }
        
        public bool Active { get; set; }
        
        public int UserOptionId { get; set; }
        
        public UserOption UserOption { get; set; }
    }
    
    public class TelegramChannelConfiguration : IEntityTypeConfiguration<TelegramChannel>
    {
        public void Configure(EntityTypeBuilder<TelegramChannel> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
            builder.HasOne(p => p.UserOption).WithMany(c => c.Channels).HasForeignKey(p => p.UserOptionId);
            
        }
    }
}