using OnionArchitectureProject.Domain.Common.BaseEntities;

namespace OnionArchitectureProject.Application.Services.CategoryService.Models.UpsertCategoryDto;
public class UpsertCategoryDto : EditableEntityBase<Guid>
{
    public string Name { get; set; } = string.Empty;
}