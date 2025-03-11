using OnionArchitectureProject.Domain.Base;
using OnionArchitectureProject.Domain.Products;

namespace OnionArchitectureProject.Domain.Categories;
public class Category : FullAuditedEntity<Guid>
{
    public string Name { get; set; } = string.Empty;

    public ICollection<Product> Products { get; set; }
}