using OnionArchitectureProject.Domain.Base.Repositories;

namespace OnionArchitectureProject.Domain.Categories;
public interface ICategoryRepository : IRepository<Category>
{
    Task<Category?> GetByCategoryNameAsync(string categoryName, CancellationToken cancellationToken);
}