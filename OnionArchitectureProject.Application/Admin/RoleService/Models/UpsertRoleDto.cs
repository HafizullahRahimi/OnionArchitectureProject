namespace OnionArchitectureProject.Application.Admin.RoleService.Models;
public class UpsertRoleDto
{
    public string? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}