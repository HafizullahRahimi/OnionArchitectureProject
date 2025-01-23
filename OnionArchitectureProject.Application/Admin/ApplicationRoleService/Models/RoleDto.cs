namespace OnionArchitectureProject.Application.Admin.ApplicationRoleService.Models;
public class RoleDto
{
    public string Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string CreatedByUserName { get; set; } = string.Empty;
    public DateTime CreatedLocalTime { get; set; }
    public string ModifiedByUserName { get; set; } = string.Empty;
    public DateTime ModifiedLocalTime { get; set; }
}