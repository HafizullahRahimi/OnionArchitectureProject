using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionArchitectureProject.Domain.Categories;

namespace OnionArchitectureProject.Persistence.Models.Categories;
internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        // Unikt index på Name
        builder
            .HasIndex(c => c.Name)
            .IsUnique();

        // Index för soft delete
        builder
            .HasIndex(c => c.IsDeleted)
            .HasFilter("IsDeleted = 0");

        // Global query filter (soft delete)
        builder
            .HasQueryFilter(c => !c.IsDeleted);
    }
}