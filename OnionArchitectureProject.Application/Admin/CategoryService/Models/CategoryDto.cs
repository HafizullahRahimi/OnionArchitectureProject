using OnionArchitectureProject.Domain.Categories;

namespace OnionArchitectureProject.Application.Admin.CategoryService.Models;
public class CategoryDto : Category
{
    public string CreatedByUserName { get; set; } = string.Empty;
    public string? ModifiedByUserName { get; set; }
}