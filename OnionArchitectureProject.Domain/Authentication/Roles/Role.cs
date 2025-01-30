using OnionArchitectureProject.Domain.Base;

namespace OnionArchitectureProject.Application.Authentication.Roles;
public class Role : EditableAuthEntityBase
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}