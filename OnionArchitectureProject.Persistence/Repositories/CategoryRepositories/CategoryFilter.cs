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
           (createdUtcDate == null || DateOnly.FromDateTime(c.CreatedUtcDate) == createdUtcDate) &&
           (createdUtcTime == null ||
                (c.CreatedUtcDate.Hour == createdUtcTime.Value.Hour &&
                 c.CreatedUtcDate.Minute == createdUtcTime.Value.Minute &&
                 c.CreatedUtcDate.Second == createdUtcTime.Value.Second)) &&
           (modifiedBy == null || c.ModifiedBy.Equals(modifiedBy)) &&
           (modifiedUtcDate == null || DateOnly.FromDateTime(c.ModifiedUtcDate) == modifiedUtcDate) &&
           (modifiedUtcTime == null ||
                (c.ModifiedUtcDate.Hour == modifiedUtcTime.Value.Hour &&
                 c.ModifiedUtcDate.Minute == modifiedUtcTime.Value.Minute &&
                 c.ModifiedUtcDate.Second == modifiedUtcTime.Value.Second));
    }
}