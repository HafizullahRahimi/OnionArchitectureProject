using OnionArchitecture.Domain.Common.Repositories;

namespace OnionArchitecture.Domain.Products;
public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetAllWithCategoryAsync(CancellationToken cancellationToken);
    Task<Product?> GetByIdWithCategoryAsync(Guid id, CancellationToken cancellationToken);
}