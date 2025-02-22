using OnionArchitectureProject.Domain.Base.Repositories;

namespace OnionArchitectureProject.Domain.Categories;
public interface ICategoryRepository : IRepository<Category>, ICategoryRepositoryWithFilter
{
    Task<bool> ExistAsync(string categoryName, CancellationToken cancellationToken);
}