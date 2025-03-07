using OnionArchitectureProject.Domain.Base.Repositories;
using OnionArchitectureProject.Domain.Categories;
using System.Linq.Expressions;

namespace OnionArchitectureProject.Persistence.Repositories.CategoryRepositories;

class CategoryFilter : IFilter<Category>
{
    private readonly string? name;
    private readonly string? createdBy;
    private readonly DateTime? createdAt;
    private readonly string? modifiedBy;
    private readonly DateTime? modifiedAt;

    public CategoryFilter(string? name, string? createdBy, DateTime? createdAt, string? modifiedBy, DateTime? modifiedAt)
    {
        this.name = name;
        this.createdBy = createdBy;
        this.createdAt = createdAt;
        this.modifiedBy = modifiedBy;
        this.modifiedAt = modifiedAt;
    }
    public Expression<Func<Category, bool>> ToExpression()
    {
        return c =>
           (name == null || c.Name == name) &&
           (createdBy == null || c.CreatedBy == createdBy) &&
           (createdAt == null ||
           c.CreatedDateUtc.Date == createdAt.Value.Date &&
            c.CreatedDateUtc.Hour == createdAt.Value.Hour &&
            c.CreatedDateUtc.Minute == createdAt.Value.Minute &&
            c.CreatedDateUtc.Second == createdAt.Value.Second) &&
           (modifiedBy == null || c.ModifiedBy == modifiedBy) &&
           (modifiedAt == null || c.ModifiedDateUtc == modifiedAt);
    }
}