using OnionArchitectureProject.Domain.Base;

namespace OnionArchitectureProject.Domain.Authentication.Roles;
public class Role : FullAuditedEntity<string>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}