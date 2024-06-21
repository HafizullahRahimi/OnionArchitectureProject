using Microsoft.EntityFrameworkCore;
using OnionArchitecture.Domain.Entities;
using OnionArchitectureProject.Application.Contracts.Persistence.IRepositories.Common;

namespace OnionArchitectureProject.Persistence.Repositories;
public class ProductIncludes : IIncludes<Product>
{
    public IQueryable<Product> ApplyIncludes(IQueryable<Product> query)
    {
        return query.Include(p => p.Category);
    }
}