using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities
{
    public class  Category : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int? ParentCategoryId { get; set; }
        
        [ForeignKey(nameof(ParentCategoryId))]
        public Category? ParentCategory { get; set; }
        
        
        public int UserOptionId { get; set; }
        
        [ForeignKey(nameof(UserOptionId))]
        public UserOption UserOption { get; set; }
        
        
        public ICollection<Category>? ChildCategories { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
    
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(200);

            builder.HasMany(p => p.Posts).WithOne(c => c.Category).HasForeignKey(p => p.CategoryId);
            builder.HasOne(p => p.UserOption).WithMany(c => c.Categories).HasForeignKey(p => p.UserOptionId);
            builder.HasMany(p => p.ChildCategories).WithOne(c => c.ParentCategory).HasForeignKey(p => p.ParentCategoryId);
        }
    }
}
