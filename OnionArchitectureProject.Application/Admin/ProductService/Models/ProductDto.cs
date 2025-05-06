using OnionArchitectureProject.Domain.Authentication.Users;
using OnionArchitectureProject.Domain.Categories;

namespace OnionArchitectureProject.Application.Admin.ProductService.Models;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Category Category { get; set; } = null!;
    public User CreatedBy { get; set; } = null!;
    public DateTime CreatedLocalTime { get; set; }
    public User? ModifiedBy { get; set; }
    public DateTime ModifiedLocalTime { get; set; }
}