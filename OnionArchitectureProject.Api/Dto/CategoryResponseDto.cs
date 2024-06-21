using OnionArchitecture.Domain.Common;

namespace OnionArchitectureProject.Api.Dto;

public class CategoryResponseDto : EditableEntityBase<Guid>
{
    public string Name { get; set; } = string.Empty;
}
