using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities
{
    public class Payment : BaseEntity
    {
        public Payment()
        {
            Active = true;
            TypePayment = TypePayment.NoCash;
        }
        
        public string Name { get; set; }
        public bool Active { get; set; }
        
        public TypePayment TypePayment { get; set; }
     
        
        public ICollection<Order> Orders { get; set; }
        public ICollection<UserOption> UserOptions { get; set; }
    }

    public enum TypePayment
    {
        [Display(Name = "Наличный")]
        Cash = 1,
        
        [Display(Name = "Безналичный")]
        NoCash = 2
    }
    
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(p => p.Name).IsRequired();

            builder.HasMany(p => p.UserOptions).WithMany(p => p.Payments);
        }
    }
}