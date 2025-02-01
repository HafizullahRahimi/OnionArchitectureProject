using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionArchitectureProject.Domain.Products;

namespace OnionArchitectureProject.Persistence.Models.Products;
internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .Property(p => p.Price)
            .HasPrecision(18, 2);

        builder
            .HasOne(p => p.Category)
            .WithMany(p => p.Products)
            .HasForeignKey(b => b.CategoryId);

        builder
            .HasIndex(_ => _.IsDeleted)
            .HasFilter("IsDeleted = 0");

        builder
            .HasQueryFilter(_ => !_.IsDeleted);
    }
}