using OnionArchitecture.Domain.Common.DataEntities;
using OnionArchitecture.Domain.Products;

namespace OnionArchitecture.Domain.Categories;
public class Category : EditableEntityBase<Guid>
{
    public string Name { get; set; } = string.Empty;

    public ICollection<Product> Products { get; set; }
}