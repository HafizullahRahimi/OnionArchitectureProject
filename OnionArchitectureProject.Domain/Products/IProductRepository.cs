using OnionArchitectureProject.Domain.Common.Repositories;

namespace OnionArchitectureProject.Domain.Products;
public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetAllWithCategoryAsync(CancellationToken cancellationToken);
    Task<Product?> GetByIdWithCategoryAsync(Guid id, CancellationToken cancellationToken);
}