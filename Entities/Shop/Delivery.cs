using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities
{
    public class Delivery : BaseEntity
    {
        public Delivery()
        {
            Active = true;
        }
        
        public string Name { get; set; }
        public bool Active { get; set; }
        
        public ICollection<UserOption> UserOptions { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
    
    public class DeliveryConfiguration : IEntityTypeConfiguration<Delivery>
    {
        public void Configure(EntityTypeBuilder<Delivery> builder)
        {
            builder.Property(p => p.Name).IsRequired();

            builder.HasMany(p => p.UserOptions).WithMany(p => p.Deliveries);
        }
    }
}