using System.Linq.Expressions;
using OnionArchitectureProject.Domain.Base.Repositories;
using OnionArchitectureProject.Domain.Products;

namespace OnionArchitectureProject.Persistence.Repositories.ProductRepositories;

public class ProductFilter : IFilter<Product>
{
    public Guid? ProductId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? CategoryName { get; set; }
    public Guid? CategoryId { get; set; }
    public string? CreatedBy { get; set; }
    public DateOnly? CreatedUtcDate { get; set; }
    public TimeOnly? CreatedUtcTime { get; set; }
    public string? ModifiedBy { get; set; }
    public DateOnly? ModifiedUtcDate { get; set; }
    public TimeOnly? ModifiedUtcTime { get; set; }

    public Expression<Func<Product, bool>> ToExpression()
    {
        return p =>
            (ProductId == null || p.Id == ProductId) &&
            (Name == null || p.Name.ToLower() == Name.ToLower()) &&
            (Description == null || p.Description.ToLower() == Description.ToLower()) &&
            (MinPrice == null || p.Price >= MinPrice) &&
            (MaxPrice == null || p.Price <= MaxPrice) &&
            (CategoryName == null || p.Category.Name.ToLower() == CategoryName.ToLower()) &&
            (CategoryId == null || p.CategoryId == CategoryId) &&
            (CreatedBy == null || p.CreatedBy.ToLower() == CreatedBy.ToLower()) &&
            (CreatedUtcDate == null || DateOnly.FromDateTime(p.CreatedUtcDate) == CreatedUtcDate) &&
            (CreatedUtcTime == null || IsTimeMatch(p.CreatedUtcDate, CreatedUtcTime.Value)) &&
            (ModifiedBy == null || p.ModifiedBy.ToLower() == ModifiedBy.ToLower()) &&
            (ModifiedUtcDate == null || DateOnly.FromDateTime(p.ModifiedUtcDate) == ModifiedUtcDate) &&
            (ModifiedUtcTime == null || IsTimeMatch(p.ModifiedUtcDate, ModifiedUtcTime.Value));
    }

    private static bool IsTimeMatch(DateTime dateTime, TimeOnly time)
    {
        return dateTime.Hour == time.Hour &&
               dateTime.Minute == time.Minute &&
               dateTime.Second == time.Second;
    }
}