using Microsoft.EntityFrameworkCore;
using OnionArchitectureProject.Domain.Base.Repositories;
using OnionArchitectureProject.Domain.Products;

namespace OnionArchitectureProject.Persistence.Models.Products;
public class ProductIncludes : IIncludes<Product>
{
    public IQueryable<Product> ApplyIncludes(IQueryable<Product> query)
    {
        return query.Include(p => p.Category);
    }
}