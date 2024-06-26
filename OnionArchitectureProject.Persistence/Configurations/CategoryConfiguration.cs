using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionArchitectureProject.Domain.Categories;

namespace OnionArchitectureProject.Persistence.Configurations;
internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder
            .HasIndex(_ => _.IsDeleted)
            .HasFilter("IsDeleted = 0");

        builder
            .HasQueryFilter(_ => !_.IsDeleted);
    }
}