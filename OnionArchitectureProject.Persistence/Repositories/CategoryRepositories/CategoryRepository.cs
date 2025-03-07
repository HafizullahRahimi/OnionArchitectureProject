using Microsoft.EntityFrameworkCore;
using OnionArchitectureProject.Domain.Categories;

namespace OnionArchitectureProject.Persistence.Repositories.CategoryRepositories;
public class CategoryRepository : CategoryRepositoryWithFilter, ICategoryRepository
{
    public CategoryRepository(IDbContextFactory<ApplicationDbContext> dbcontextFactory) : base(dbcontextFactory) { }

    public async Task<bool> ExistAsync(string categoryName, CancellationToken cancellationToken)
    {
        var entity = await GetByNameAsync(categoryName, cancellationToken);
        return entity != null;
    }

    private async Task<Category?> GetByNameAsync(string categoryName, CancellationToken cancellationToken)
    {
        return await DbContext.Categories
            .IgnoreQueryFilters()
            .Where(c => c.Name == categoryName)
            .FirstOrDefaultAsync(cancellationToken);
    }
}