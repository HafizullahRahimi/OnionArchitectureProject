using OnionArchitectureProject.Domain.Authentication.Roles;

namespace OnionArchitectureProject.Application.Admin.RoleService;
public interface IRoleValidationService
{
    Task<bool> IsRoleNameUniqueAsync(string roleName, CancellationToken cancellationToken);
    Task<Role?> GetRoleByIdAsync(string roleId, CancellationToken cancellationToken);
}