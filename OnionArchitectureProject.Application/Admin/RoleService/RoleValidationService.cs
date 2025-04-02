using Microsoft.Extensions.DependencyInjection;
using OnionArchitectureProject.Domain.Authentication.Roles;

namespace OnionArchitectureProject.Application.Admin.RoleService;
public class RoleValidationService(IServiceScopeFactory scopeFactory) : IRoleValidationService
{
    private readonly IServiceScopeFactory scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));

    public async Task<bool> IsRoleNameUniqueAsync(string roleName, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(roleName))
            return false;

        using var scope = scopeFactory.CreateScope();
        var roleRepository = scope.ServiceProvider.GetRequiredService<IRoleRepository>();
        var roleNameExists = await roleRepository.RoleNameExistsAsync(roleName);
        return roleNameExists == false;
    }

    public async Task<Role?> GetRoleByIdAsync(string roleId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(roleId))
            return null;

        using var scope = scopeFactory.CreateScope();
        var roleRepository = scope.ServiceProvider.GetRequiredService<IRoleRepository>();
        return await roleRepository.GetByIdAsync(roleId);
    }
}