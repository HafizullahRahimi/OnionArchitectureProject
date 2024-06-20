using OnionArchitecture.Domain.Entities;
using OnionArchitectureProject.Application.Contracts.Persistence.IRepositories.Common;

namespace OnionArchitectureProject.Application.Contracts.Persistence.IRepositories;
public interface IProductRepository : IRepository<Product>
{

    Task<Product?> GetByIdWithCategoryAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Product>> GetByAllWithCategoryAsync(CancellationToken cancellationToken);
}