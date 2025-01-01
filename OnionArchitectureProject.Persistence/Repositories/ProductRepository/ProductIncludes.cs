using Microsoft.EntityFrameworkCore;
using OnionArchitectureProject.Domain.Base.Repositories;

namespace OnionArchitectureProject.Domain.Products.Repositories;
public class ProductIncludes : IIncludes<Product>
{
    public IQueryable<Product> ApplyIncludes(IQueryable<Product> query)
    {
        return query.Include(p => p.Category);
    }
}