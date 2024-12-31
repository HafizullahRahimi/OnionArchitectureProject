using OnionArchitectureProject.Domain.Categories;

namespace OnionArchitectureProject.Application.Services.CategoryService.Models;
public class CategoryDto : Category
{
    public string CreatedByUserName { get; set; } = string.Empty;
    public string? ModifiedByUserName { get; set; }
}