using Microsoft.EntityFrameworkCore;
using OnionArchitectureProject.Domain.Base.Repositories;
using OnionArchitectureProject.Domain.Categories;

namespace OnionArchitectureProject.Persistence.Repositories.CategoryRepositories;

public class CategoryIncludes : IIncludes<Category>
{
    public IQueryable<Category> ApplyIncludes(IQueryable<Category> query)
    {
        return query.Include(c => c.Products);
    }
}