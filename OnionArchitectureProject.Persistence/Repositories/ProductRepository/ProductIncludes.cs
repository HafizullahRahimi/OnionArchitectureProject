using Microsoft.EntityFrameworkCore;
using OnionArchitectureProject.Domain.Common.Repositories;

namespace OnionArchitectureProject.Domain.Products.Repositories;
public class ProductIncludes : IIncludes<Product>
{
    public IQueryable<Product> ApplyIncludes(IQueryable<Product> query)
    {
        return query.Include(p => p.Category);
    }
}