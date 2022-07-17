#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities
{
    public class CategoryProduct : BaseEntity
    {
        [MinLength(10)]
        public string Name { get; set; }
        public string? Description { get; set; }
        
        public int UserOptionId { get; set; }
        public UserOption UserOption { get; set; }
        public ICollection<Product> Products { get; set; }
    }
    
    public class CategoryProductConfiguration : IEntityTypeConfiguration<CategoryProduct>
    {
        public void Configure(EntityTypeBuilder<CategoryProduct> builder)
        {
            builder.Property(p => p.Name).IsRequired();

            builder
                .HasOne(p => p.UserOption)
                .WithMany(c => c.CategoryProducts)
                .HasForeignKey(p => p.UserOptionId);
        }
    }
    
}