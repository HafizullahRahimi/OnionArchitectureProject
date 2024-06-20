using Microsoft.EntityFrameworkCore;
using OnionArchitecture.Domain.Entities;
using OnionArchitectureProject.Application.Contracts.Persistence.IRepositories;

namespace OnionArchitectureProject.Persistence.Repositories;
public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public virtual async Task<Product?> GetByIdAsync(Guid id)
    {
        return await DbContext.Products
            .Include(p => p.Category)
            .SingleOrDefaultAsync(c => c.Id == id);
    }

}