using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        
        public int ProductId { get; set; }
        public Product Product { get; set; }
        
        public double Amount { get; set; }
        public double Price { get; set; }
    }
    
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder
                .Property(p => p.Amount)
                .IsRequired();
            
            builder
                .Property(p => p.Price)
                .IsRequired();
            
            builder
                .HasOne(p => p.Product)
                .WithMany(c => c.OrderItems)
                .HasForeignKey(p => p.ProductId);

        }
    }
}