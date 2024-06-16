using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnionArchitecture.Domain.Entities;
using System.Reflection.Emit;

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