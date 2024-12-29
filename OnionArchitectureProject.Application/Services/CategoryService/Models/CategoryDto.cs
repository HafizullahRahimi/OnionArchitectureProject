namespace OnionArchitectureProject.Application.Services.CategoryService.Models;
public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string CreatedByUserName { get; set; } = string.Empty;
    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedByUserName { get; set; }
}