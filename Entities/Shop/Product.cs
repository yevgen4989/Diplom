using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities
{
    public class Product : BaseEntity
    {
        public Product()
        {
            Type = TypeProduct.Common;
            IsOffer = false;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        
        public double Price { get; set; }
        
        public TypeProduct Type { get; set; }
        public bool IsOffer { get; set; }
        
        public int UserOptionId { get; set; }
        public UserOption UserOption { get; set; }
        
        public int? ParentProductId { get; set; }
        [ForeignKey(nameof(ParentProductId))]
        public Product ParentProduct { get; set; }
        
        
        public ICollection<Product>? ChildProducts { get; set; }
        public ICollection<CategoryProduct>? Categories { get; set; }
        public ICollection<PropertyValue>? PropertiesValues { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
    }

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired();

            builder
                .HasMany(p => p.ChildProducts)
                .WithOne(c => c.ParentProduct)
                .HasForeignKey(p => p.ParentProductId);
            
            builder
                .HasMany(p => p.PropertiesValues)
                .WithOne(c => c.Product)
                .HasForeignKey(p => p.ProductId);
            
            builder
                .HasOne(p => p.UserOption)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.UserOptionId);

            builder
                .HasMany(p => p.Categories)
                .WithMany(c => c.Products);
        }
    }
    
    public enum TypeProduct
    {
        [Display(Name = "Простой")]
        Common = 1,

        [Display(Name = "Торговые предложения")]
        Offers = 2
    }
}