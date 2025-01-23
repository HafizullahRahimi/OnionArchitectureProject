namespace OnionArchitectureProject.Application.Admin.CategoryService.Models;
public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CreatedByUserName { get; set; } = string.Empty;
    public DateTime CreatedLocalTime { get; set; }
    public string ModifiedByUserName { get; set; } = string.Empty;
    public DateTime ModifiedLocalTime { get; set; }
}