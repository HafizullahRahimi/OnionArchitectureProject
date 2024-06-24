using OnionArchitecture.Domain.Common.BaseEntities;

namespace OnionArchitectureProject.Application.Services.CategoryService.Models;
public class CategoryDto : EditableEntityBase<Guid>
{
    public string Name { get; set; } = string.Empty;
}