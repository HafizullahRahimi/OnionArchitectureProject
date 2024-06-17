using OnionArchitecture.Domain.Common;

namespace OnionArchitecture.Domain.Entities;
public class Category : SoftdeleteableEntityBase<Guid>
{
    public string Name { get; set; } = string.Empty;

    public ICollection<Product> Products { get; set; }
}