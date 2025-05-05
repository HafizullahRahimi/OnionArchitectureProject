namespace OnionArchitectureProject.Domain.Products;

public interface IProductRepositoryWithFilter
{
    IProductRepositoryWithFilter WithProductId(Guid productId);
    IProductRepositoryWithFilter WithName(string name);
    IProductRepositoryWithFilter WithDescription(string description);
    IProductRepositoryWithFilter WithPriceRange(decimal minPrice, decimal maxPrice);
    IProductRepositoryWithFilter WithCategoryName(string categoryName);
    IProductRepositoryWithFilter WithCategoryId(Guid categoryId);
    IProductRepositoryWithFilter WithCategory();
    IProductRepositoryWithFilter WithCreatedBy(string userId);
    IProductRepositoryWithFilter WithCreatedAtUtcDate(DateOnly utcDate);
    IProductRepositoryWithFilter WithCreatedAtUtcTime(TimeOnly utcTime);
    IProductRepositoryWithFilter WithModifiedBy(string userId);
    IProductRepositoryWithFilter WithModifiedAtUtcDate(DateOnly utcDate);
    IProductRepositoryWithFilter WithModifiedAtUtcTime(TimeOnly utcTime);
    Task<List<Product>> ToListAsync(CancellationToken cancellationToken);
}