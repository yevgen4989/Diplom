using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Entities
{
    public class UserOption : BaseEntity<int>
    {
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string FirebaseId { get; set; }

        public string DisplayName { get; set; } = "User #" + (new Random()).NextInt64();

        public DateTimeOffset? LastLoginDate { get; set; }

        public string? UsernameTelegram { get; set; }
        
        
        
           
        
        
        
        public ICollection<Post> Posts { get; set; }
        public ICollection<PostFile> PostFiles { get; set; }
        public ICollection<Category> Categories { get; set; }


        public ICollection<Order> Orders { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<CategoryProduct> CategoryProducts { get; set; }
        public ICollection<Property> Properties { get; set; }
        public ICollection<Currency> Currencies { get; set; }
        public ICollection<Delivery> Deliveries { get; set; }
        public ICollection<Payment> Payments { get; set; }

        public ICollection<TelegramChannel> Channels { get; set; }
    }

    public class UserConfiguration : IEntityTypeConfiguration<UserOption>
    {
        public void Configure(EntityTypeBuilder<UserOption> builder)
        {
            // builder.Property(p => p.UserName).IsRequired().HasMaxLength(100);

            // builder
            //     .HasOne(p => p.User)
            //     .WithMany(c => c.Channels)
            //     .HasForeignKey(p => p.UserId);

            builder
                .HasMany(p => p.Channels)
                .WithOne(p => p.UserOption)
                .HasForeignKey(p => p.UserOptionId);

        }
    }
}
