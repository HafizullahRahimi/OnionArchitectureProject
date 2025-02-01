using Microsoft.EntityFrameworkCore;
using OnionArchitectureProject.Domain.Categories;
using OnionArchitectureProject.Persistence.Models.Base.Repositories;

namespace OnionArchitectureProject.Persistence.Models.Categories;
public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(IDbContextFactory<ApplicationDbContext> dbcontextFactory) : base(dbcontextFactory)
    {
    }

    public async Task<Category?> GetByCategoryNameAsync(string categoryName, CancellationToken cancellationToken)
    {
        return await DbContext.Categories
            .IgnoreQueryFilters()
            .Where(c => c.Name == categoryName)
            .FirstOrDefaultAsync(cancellationToken);
    }
}