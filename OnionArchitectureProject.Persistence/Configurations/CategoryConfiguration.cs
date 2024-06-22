using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using OnionArchitecture.Domain.Categories;

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