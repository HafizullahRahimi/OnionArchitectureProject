using OnionArchitecture.Domain.Entities;
using OnionArchitectureProject.Application.Contracts.Persistence.IRepositories;
using OnionArchitectureProject.Persistence.Repositories.Common;

namespace OnionArchitectureProject.Persistence.Repositories.ProductRepository;
public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public new virtual async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var filter = new Filter<Product>(p => p.Id == id);
        var includes = new ProductIncludes();
        return await GetAsync(filter, cancellationToken, includes);
    }

    public new virtual async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        var includes = new ProductIncludes();
        return await GetAllAsync(includes, cancellationToken);
    }
}