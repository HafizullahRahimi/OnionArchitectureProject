using OnionArchitecture.Domain.Common;

namespace OnionArchitectureProject.Api.Dto;

public class CategoryResponseDto : SoftdeleteableEntityBase<Guid>
{
    public string Name { get; set; } = string.Empty;
}
