using OnionArchitecture.Domain.Products;
using OnionArchitecture.Domain.Products.Repositories;
using OnionArchitectureProject.Persistence.Repositories.Common;

namespace OnionArchitectureProject.Persistence.Repositories.ProductRepository;
public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<Product?> GetByIdWithCategoryAsync(Guid id, CancellationToken cancellationToken)
    {
        var filter = new Filter<Product>(p => p.Id == id);
        var includes = new ProductIncludes();
        return await GetAsync(filter, cancellationToken, includes);
    }

    public async Task<List<Product>> GetAllWithCategoryAsync(CancellationToken cancellationToken)
    {
        var includes = new ProductIncludes();
        return await GetAllAsync(includes, cancellationToken);
    }
}