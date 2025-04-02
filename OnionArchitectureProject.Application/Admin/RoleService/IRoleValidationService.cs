namespace OnionArchitectureProject.Application.Admin.RoleService;
public interface IRoleValidationService
{
    Task<bool> IsRoleNameUniqueAsync(string roleName, CancellationToken cancellationToken);
}