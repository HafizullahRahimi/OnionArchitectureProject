using OnionArchitectureProject.Domain.Categories;
using OnionArchitectureProject.Persistence.Repositories.Common;

namespace OnionArchitectureProject.Persistence.Repositories.CategoryRepository;
public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}