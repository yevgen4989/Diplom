using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Entities
{
    public class Order : BaseEntity
    {
        public int AccountNumber { get; set; }
        
        public string MobileNumber { get; set; }
        
        public string ClientName { get; set; }
        
        public ICollection<OrderItem> OrderItems { get; set; }
        
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }
        
        public int DeliveryId { get; set; }
        public Delivery Delivery { get; set; }

        public int UserOptionId { get; set; }
        public UserOption UserOption { get; set; }
    }
    
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .Property(p => p.AccountNumber)
                .IsRequired();
            
            builder
                .Property(p => p.MobileNumber)
                .IsRequired();

            builder
                .HasMany(p => p.OrderItems)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId);
            
            builder
                .HasOne(p => p.UserOption)
                .WithMany(c => c.Orders)
                .HasForeignKey(p => p.UserOptionId);

            builder
                .HasOne(p => p.Payment)
                .WithMany(p => p.Orders)
                .HasForeignKey(p => p.PaymentId);
            
            builder
                .HasOne(p => p.Delivery)
                .WithMany(p => p.Orders)
                .HasForeignKey(p => p.DeliveryId);
            
        }
    }
}