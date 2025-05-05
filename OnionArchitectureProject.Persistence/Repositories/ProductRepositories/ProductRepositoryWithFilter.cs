using Microsoft.EntityFrameworkCore;
using OnionArchitectureProject.Domain.Base.Repositories;
using OnionArchitectureProject.Domain.Products;
using OnionArchitectureProject.Persistence.Repositories.Base;

namespace OnionArchitectureProject.Persistence.Repositories.ProductRepositories;

public class ProductRepositoryWithFilter : Repository<Product>, IProductRepositoryWithFilter
{
    private ProductFilter Filter { get; }
    private IIncludes<Product>? IncludeCategory { get; set; }

    protected ProductRepositoryWithFilter(IDbContextFactory<ApplicationDbContext> dbcontextFactory)
        : base(dbcontextFactory)
    {
        Filter = new ProductFilter();
    }

    public IProductRepositoryWithFilter WithProductId(Guid productId)
    {
        Filter.ProductId = productId;
        return this;
    }

    public IProductRepositoryWithFilter WithName(string name)
    {
        Filter.Name = name;
        return this;
    }

    public IProductRepositoryWithFilter WithDescription(string description)
    {
        Filter.Description = description;
        return this;
    }

    public IProductRepositoryWithFilter WithPriceRange(decimal minPrice, decimal maxPrice)
    {
        Filter.MinPrice = minPrice;
        Filter.MaxPrice = maxPrice;
        return this;
    }

    public IProductRepositoryWithFilter WithCategoryName(string categoryName)
    {
        Filter.CategoryName = categoryName;
        return this;
    }

    public IProductRepositoryWithFilter WithCategoryId(Guid categoryId)
    {
        Filter.CategoryId = categoryId;
        return this;
    }

    public IProductRepositoryWithFilter WithCategory()
    {
        IncludeCategory = new ProductIncludes();
        return this;
    }

    public IProductRepositoryWithFilter WithCreatedBy(string userId)
    {
        Filter.CreatedBy = userId;
        return this;
    }

    public IProductRepositoryWithFilter WithCreatedAtUtcDate(DateOnly utcDate)
    {
        Filter.CreatedUtcDate = utcDate;
        return this;
    }

    public IProductRepositoryWithFilter WithCreatedAtUtcTime(TimeOnly utcTime)
    {
        Filter.CreatedUtcTime = utcTime;
        return this;
    }

    public IProductRepositoryWithFilter WithModifiedBy(string userId)
    {
        Filter.ModifiedBy = userId;
        return this;
    }

    public IProductRepositoryWithFilter WithModifiedAtUtcDate(DateOnly utcDate)
    {
        Filter.ModifiedUtcDate = utcDate;
        return this;
    }

    public IProductRepositoryWithFilter WithModifiedAtUtcTime(TimeOnly utcTime)
    {
        Filter.ModifiedUtcTime = utcTime;
        return this;
    }

    public async Task<List<Product>> ToListAsync(CancellationToken cancellationToken)
    {
        return await GetAllAsync(Filter, IncludeCategory, cancellationToken);
    }
}