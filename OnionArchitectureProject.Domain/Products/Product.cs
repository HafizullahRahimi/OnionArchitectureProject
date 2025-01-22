using OnionArchitectureProject.Domain.Base;
using OnionArchitectureProject.Domain.Categories;

namespace OnionArchitectureProject.Domain.Products;
public class Product : EditableEntityBase<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
}