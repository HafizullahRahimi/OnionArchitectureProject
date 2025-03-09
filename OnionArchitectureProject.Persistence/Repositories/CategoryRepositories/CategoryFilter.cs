using OnionArchitectureProject.Domain.Base.Repositories;
using OnionArchitectureProject.Domain.Categories;
using System.Linq.Expressions;

namespace OnionArchitectureProject.Persistence.Repositories.CategoryRepositories;

public class CategoryFilter : IFilter<Category>
{
    private readonly string? name;
    private readonly string? createdBy;
    private readonly DateOnly? createdUtcDate;
    private readonly TimeOnly? createdUtcTime;
    private readonly string? modifiedBy;
    private readonly DateOnly? modifiedUtcDate;
    private readonly TimeOnly? modifiedUtcTime;

    public CategoryFilter(
        string? name,
        string? createdBy,
        DateOnly? createdUtcDate,
        TimeOnly? createdUtcTime,
        string? modifiedBy,
        DateOnly? modifiedUtcDate,
        TimeOnly? modifiedUtcTime)
    {
        this.name = name;
        this.createdBy = createdBy;
        this.createdUtcDate = createdUtcDate;
        this.createdUtcTime = createdUtcTime;
        this.modifiedBy = modifiedBy;
        this.modifiedUtcDate = modifiedUtcDate;
        this.modifiedUtcTime = modifiedUtcTime;
    }

    public Expression<Func<Category, bool>> ToExpression()
    {
        return c =>
           (name == null || c.Name.Equals(name)) &&
           (createdBy == null || c.CreatedBy.Equals(createdBy)) &&
           (createdUtcDate == null || DateOnly.FromDateTime(c.CreatedDateUtc) == createdUtcDate) &&
           (createdUtcTime == null ||
                (c.CreatedDateUtc.Hour == createdUtcTime.Value.Hour &&
                 c.CreatedDateUtc.Minute == createdUtcTime.Value.Minute &&
                 c.CreatedDateUtc.Second == createdUtcTime.Value.Second)) &&
           (modifiedBy == null || c.ModifiedBy.Equals(modifiedBy)) &&
           (modifiedUtcDate == null || DateOnly.FromDateTime(c.ModifiedDateUtc) == modifiedUtcDate) &&
           (modifiedUtcTime == null ||
                (c.ModifiedDateUtc.Hour == modifiedUtcTime.Value.Hour &&
                 c.ModifiedDateUtc.Minute == modifiedUtcTime.Value.Minute &&
                 c.ModifiedDateUtc.Second == modifiedUtcTime.Value.Second));
    }
}