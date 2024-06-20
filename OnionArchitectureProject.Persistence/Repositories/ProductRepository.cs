using Microsoft.EntityFrameworkCore;
using OnionArchitecture.Domain.Entities;
using OnionArchitectureProject.Application.Contracts.Persistence.IRepositories;
using OnionArchitectureProject.Persistence.Repositories.Common;

namespace OnionArchitectureProject.Persistence.Repositories;
public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<List<Product>> GetByAllWithCategoryAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Products
           .Include(p => p.Category)
           .ToListAsync(cancellationToken);
    }

    //public virtual async Task<Product?> GetByIdAsync(Guid id)
    //{
    //    return await DbContext.Products
    //        .Include(p => p.Category)
    //        .SingleOrDefaultAsync(c => c.Id == id);
    //}

    public async Task<Product?> GetByIdWithCategoryAsync(Guid id, CancellationToken cancellationToken)
    {
        return await DbContext.Products
            .Include(p => p.Category)
            .SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}