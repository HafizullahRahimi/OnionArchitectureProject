namespace OnionArchitectureProject.Domain.Categories;

public interface ICategoryRepositoryWithFilter
{
    ICategoryRepositoryWithFilter WithName(string name);
    ICategoryRepositoryWithFilter WithCreatedBy(string userId);
    ICategoryRepositoryWithFilter WithModifiedBy(string userId);
    ICategoryRepositoryWithFilter WithCreatedAtUtcDate(DateOnly utcDate);
    ICategoryRepositoryWithFilter WithCreatedAtUtcTime(TimeOnly utcTime);
    ICategoryRepositoryWithFilter WithModifiedAtUtcDate(DateOnly utcDate);
    ICategoryRepositoryWithFilter WithModifiedAtUtcTime(TimeOnly utcTime);
    Task<List<Category>> ToListAsync(CancellationToken cancellationToken);
}