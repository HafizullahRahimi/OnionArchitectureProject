using OnionArchitectureProject.Domain.Authentication;

namespace OnionArchitectureProject.Application.Admin.ApplicationRoleService.Models;
public class RoleDto : ApplicationRole
{
    public string CreatedByUserName { get; set; } = string.Empty;
    public string? ModifiedByUserName { get; set; }
}