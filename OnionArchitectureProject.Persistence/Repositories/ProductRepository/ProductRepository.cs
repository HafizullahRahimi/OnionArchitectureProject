using Microsoft.EntityFrameworkCore;
using OnionArchitectureProject.Domain.Products;
using OnionArchitectureProject.Domain.Products.Repositories;
using OnionArchitectureProject.Persistence.Repositories.Common;

namespace OnionArchitectureProject.Persistence.Repositories.ProductRepository;
public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(IDbContextFactory<ApplicationDbContext> dbcontextFactory) : base(dbcontextFactory) { }

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