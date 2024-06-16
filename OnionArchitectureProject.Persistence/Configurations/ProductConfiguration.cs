using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnionArchitecture.Domain.Entities;
using System.Reflection.Emit;

namespace OnionArchitectureProject.Persistence.Configurations;
internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .Property(p => p.Price)
            .HasPrecision(18, 2);

        //Relationship One To Many
        builder
            .HasOne(p => p.Category)
            .WithMany(p => p.Products)
            .HasForeignKey(b => b.CategoryId); 


    }
}