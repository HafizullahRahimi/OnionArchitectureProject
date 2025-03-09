using Microsoft.EntityFrameworkCore;
using OnionArchitectureProject.Domain.Categories;
using OnionArchitectureProject.Persistence.Repositories.Base;

namespace OnionArchitectureProject.Persistence.Repositories.CategoryRepositories;

public class CategoryRepositoryWithFilter : Repository<Category>, ICategoryRepositoryWithFilter
{
    private string? name;
    private string? createdBy;
    private DateOnly? createdUtcDate;
    private TimeOnly? createdUtcTime;
    private string? modifiedBy;
    private DateOnly? modifiedUtcDate;
    private TimeOnly? modifiedUtcTime;

    protected CategoryRepositoryWithFilter(IDbContextFactory<ApplicationDbContext> dbcontextFactory)
        : base(dbcontextFactory)
    {
    }

    public ICategoryRepositoryWithFilter WithName(string name)
    {
        this.name = name;
        return this;
    }

    public ICategoryRepositoryWithFilter WithCreatedBy(string userId)
    {
        createdBy = userId;
        return this;
    }

    public ICategoryRepositoryWithFilter WithCreatedAtUtcDate(DateOnly utcDate)
    {
        createdUtcDate = utcDate;
        return this;
    }

    public ICategoryRepositoryWithFilter WithCreatedAtUtcTime(TimeOnly utcTime)
    {
        createdUtcTime = utcTime;
        return this;
    }

    public ICategoryRepositoryWithFilter WithModifiedBy(string userId)
    {
        modifiedBy = userId;
        return this;
    }

    public ICategoryRepositoryWithFilter WithModifiedAtUtcDate(DateOnly utcDate)
    {
        modifiedUtcDate = utcDate;
        return this;
    }

    public ICategoryRepositoryWithFilter WithModifiedAtUtcTime(TimeOnly utcTime)
    {
        modifiedUtcTime = utcTime;
        return this;
    }

    public async Task<List<Category>> ToListAsync(CancellationToken cancellationToken)
    {
        var filter = new CategoryFilter(
            name,
            createdBy,
            createdUtcDate,
            createdUtcTime,
            modifiedBy,
            modifiedUtcDate,
            modifiedUtcTime);
        return await GetAllAsync(filter, cancellationToken);
    }
}