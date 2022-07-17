using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities
{
    public class PropertyValue : BaseEntity
    {
        public string Value { get; set; }

        public int PropertyId { get; set; }
        public Property Property { get; set; }
        
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
    
    public class PropertyValueConfiguration : IEntityTypeConfiguration<PropertyValue>
    {
        public void Configure(EntityTypeBuilder<PropertyValue> builder)
        {
            builder.Property(p => p.Value).IsRequired();
            
            builder
                .HasOne(p => p.Property)
                .WithMany(c => c.PropertyValues)
                .HasForeignKey(p => p.PropertyId);
            
            
        }
    }
}