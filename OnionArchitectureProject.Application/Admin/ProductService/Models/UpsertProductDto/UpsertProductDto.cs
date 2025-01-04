namespace OnionArchitectureProject.Application.Admin.ProductService.Models.UpsertProductDto;
public class UpsertProductDto
{
    public string Name { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string CategoryId { get; set; } = string.Empty;
}