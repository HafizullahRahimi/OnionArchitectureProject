namespace OnionArchitectureProject.Application.Services.ProductService.Models;
public class CreateProductDto
{
    public string Name { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
}