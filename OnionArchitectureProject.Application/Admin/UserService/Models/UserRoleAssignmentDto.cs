namespace OnionArchitectureProject.Application.Admin.UserService.Models;
public class UserRoleAssignmentDto
{
    public string UserId { get; set; } = string.Empty;
    public List<RoleCheckbox> Roles { get; set; } = new List<RoleCheckbox>();
}