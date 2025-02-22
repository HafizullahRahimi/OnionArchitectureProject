namespace OnionArchitectureProject.Domain.Categories;

public interface ICategoryRepositoryWithFilter
{
    ICategoryRepositoryWithFilter WithName(string name);
    ICategoryRepositoryWithFilter WithCreatedBy(string userId);
    ICategoryRepositoryWithFilter WithCreatedAt(DateTime date);
    ICategoryRepositoryWithFilter WithModifiedBy(string userId);
    ICategoryRepositoryWithFilter WithModifiedAt(DateTime date);
    Task<List<Category>> ToListAsync(CancellationToken cancellationToken);
}