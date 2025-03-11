using OnionArchitectureProject.Domain.Base;

namespace OnionArchitectureProject.Application.Admin.ProductService.Models;
public class ProductDto : FullAuditedEntity<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
}