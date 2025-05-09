using Microsoft.EntityFrameworkCore;
using OnionArchitectureProject.Domain.Base.Repositories;
using OnionArchitectureProject.Domain.Categories;
using OnionArchitectureProject.Persistence.Repositories.Base;

namespace OnionArchitectureProject.Persistence.Repositories.CategoryRepositories;

public class CategoryRepositoryWithFilter : Repository<Category>, ICategoryRepositoryWithFilter
{
    private CategoryFilter Filter { get; }
    private IIncludes<Category>? IncludeProducts { get; set; }

    protected CategoryRepositoryWithFilter(IDbContextFactory<ApplicationDbContext> dbcontextFactory)
        : base(dbcontextFactory)
    {
        Filter = new CategoryFilter();
    }

    public ICategoryRepositoryWithFilter WithName(string name)
    {
        Filter.Name = name;
        return this;
    }

    public ICategoryRepositoryWithFilter WithProducts()
    {
        IncludeProducts = new CategoryIncludes();
        return this;
    }

    public ICategoryRepositoryWithFilter WithCreatedBy(string userId)
    {
        Filter.CreatedBy = userId;
        return this;
    }

    public ICategoryRepositoryWithFilter WithCreatedAtUtcDate(DateOnly utcDate)
    {
        Filter.CreatedUtcDate = utcDate;
        return this;
    }

    public ICategoryRepositoryWithFilter WithCreatedAtUtcTime(TimeOnly utcTime)
    {
        Filter.CreatedUtcTime = utcTime;
        return this;
    }

    public ICategoryRepositoryWithFilter WithModifiedBy(string userId)
    {
        Filter.ModifiedBy = userId;
        return this;
    }

    public ICategoryRepositoryWithFilter WithModifiedAtUtcDate(DateOnly utcDate)
    {
        Filter.ModifiedUtcDate = utcDate;
        return this;
    }

    public ICategoryRepositoryWithFilter WithModifiedAtUtcTime(TimeOnly utcTime)
    {
        Filter.ModifiedUtcTime = utcTime;
        return this;
    }

    public async Task<List<Category>> ToListAsync(CancellationToken cancellationToken)
    {
        return await GetAllAsync(Filter, IncludeProducts, cancellationToken);
    }
}