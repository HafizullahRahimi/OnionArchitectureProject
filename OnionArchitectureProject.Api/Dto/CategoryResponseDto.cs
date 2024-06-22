using OnionArchitecture.Domain.Common.BaseEntities;

namespace OnionArchitectureProject.Api.Dto;
public class CategoryResponseDto : EditableEntityBase<Guid>
{
    public string Name { get; set; } = string.Empty;
}
