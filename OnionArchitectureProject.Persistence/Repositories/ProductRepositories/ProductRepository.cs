using Microsoft.EntityFrameworkCore;
using OnionArchitectureProject.Domain.Products;

namespace OnionArchitectureProject.Persistence.Repositories.ProductRepositories;

public class ProductRepository : ProductRepositoryWithFilter, IProductRepository
{
    public ProductRepository(IDbContextFactory<ApplicationDbContext> dbcontextFactory) : base(dbcontextFactory) { }
}