using OnionArchitecture.Domain.Common;

namespace OnionArchitecture.Domain.Entities;
public class Product : AuditableBaseEntity<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
}