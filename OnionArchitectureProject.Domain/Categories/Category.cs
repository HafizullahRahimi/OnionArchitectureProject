using OnionArchitectureProject.Domain.Common.BaseEntities;
using OnionArchitectureProject.Domain.Products;

namespace OnionArchitectureProject.Domain.Categories;
public class Category : EditableEntityBase<Guid>
{
    public string Name { get; set; } = string.Empty;

    public ICollection<Product> Products { get; set; }
}