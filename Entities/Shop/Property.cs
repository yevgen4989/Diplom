using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities
{
    public class Property : BaseEntity
    {
        public Property()
        {
            Type = TypeProperty.String;
            ForCategory = false;
            ForOffer = false;
        }
        
        public string Name { get; set; }
        public TypeProperty Type { get; set; }
        public bool ForOffer { get; set; }
        public bool ForCategory { get; set; }
        
        
        public int UserOptionId { get; set; }
        public UserOption UserOption { get; set; }
        
        public ICollection<PropertyValue> PropertyValues { get; set; }
    }

    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.Property(p => p.Name).IsRequired();

            builder
                .HasOne(p => p.UserOption)
                .WithMany(c => c.Properties)
                .HasForeignKey(p => p.UserOptionId);
        }
    }
    
    public enum TypeProperty
    {
        [Display(Name = "Строка")]
        String = 1,

        [Display(Name = "Число")]
        Number = 2,
        
        [Display(Name = "Дата")]
        Date = 3,
        
        [Display(Name = "Файл")]
        File = 4
    }
}