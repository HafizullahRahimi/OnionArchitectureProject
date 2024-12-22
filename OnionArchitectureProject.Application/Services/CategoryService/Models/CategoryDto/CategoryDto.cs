using OnionArchitectureProject.Domain.Common.BaseEntities;

namespace OnionArchitectureProject.Application.Services.CategoryService.Models.CategoryDto;
public class CategoryDto : EditableEntityBase<Guid>
{
    public string Name { get; set; } = string.Empty;
}