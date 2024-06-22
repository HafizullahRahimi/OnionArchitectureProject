using OnionArchitecture.Domain.Common.DataEntities;

namespace OnionArchitectureProject.Api.Dto;

public class CategoryResponseDto : EditableEntityBase<Guid>
{
    public string Name { get; set; } = string.Empty;
}
