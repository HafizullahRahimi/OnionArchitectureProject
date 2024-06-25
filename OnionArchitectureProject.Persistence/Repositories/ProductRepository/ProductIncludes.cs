using Microsoft.EntityFrameworkCore;
using OnionArchitecture.Domain.Common.Repositories;

namespace OnionArchitecture.Domain.Products.Repositories;
public class ProductIncludes : IIncludes<Product>
{
    public IQueryable<Product> ApplyIncludes(IQueryable<Product> query)
    {
        return query.Include(p => p.Category);
    }
}