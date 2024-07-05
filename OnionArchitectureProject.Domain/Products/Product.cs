using OnionArchitectureProject.Domain.Common.BaseEntities;
using OnionArchitectureProject.Domain.Categories;

namespace OnionArchitectureProject.Domain.Products;
public class Product : EditableEntityBase<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
}