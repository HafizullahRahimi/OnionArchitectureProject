using OnionArchitecture.Domain.Common;
using System.Security.Cryptography;

namespace OnionArchitectureProject.Api.Dto;

public class ProductResponseDto: EditableEntityBase<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
}
