using OnionArchitectureProject.Domain.Base.Repositories;
using OnionArchitectureProject.Domain.Categories;
using System.Linq.Expressions;

namespace OnionArchitectureProject.Persistence.Repositories.CategoryRepositories;

public class CategoryFilter : IFilter<Category>
{
    public string? Name { get; set; }
    public string? CreatedBy { get; set; }
    public DateOnly? CreatedUtcDate { get; set; }
    public TimeOnly? CreatedUtcTime { get; set; }
    public string? ModifiedBy { get; set; }
    public DateOnly? ModifiedUtcDate { get; set; }
    public TimeOnly? ModifiedUtcTime { get; set; }

    public Expression<Func<Category, bool>> ToExpression()
    {
        return c =>
           (Name == null || c.Name.Equals(Name)) &&
           (CreatedBy == null || c.CreatedBy.Equals(CreatedBy)) &&
           (CreatedUtcDate == null || DateOnly.FromDateTime(c.CreatedUtcDate) == CreatedUtcDate) &&
           (CreatedUtcTime == null ||
                (c.CreatedUtcDate.Hour == CreatedUtcTime.Value.Hour &&
                 c.CreatedUtcDate.Minute == CreatedUtcTime.Value.Minute &&
                 c.CreatedUtcDate.Second == CreatedUtcTime.Value.Second)) &&
           (ModifiedBy == null || c.ModifiedBy.Equals(ModifiedBy)) &&
           (ModifiedUtcDate == null || DateOnly.FromDateTime(c.ModifiedUtcDate) == ModifiedUtcDate) &&
           (ModifiedUtcTime == null ||
                (c.ModifiedUtcDate.Hour == ModifiedUtcTime.Value.Hour &&
                 c.ModifiedUtcDate.Minute == ModifiedUtcTime.Value.Minute &&
                 c.ModifiedUtcDate.Second == ModifiedUtcTime.Value.Second));
    }
}