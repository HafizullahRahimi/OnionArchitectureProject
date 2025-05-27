using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnionArchitectureProject.Authentication.Models;
using OnionArchitectureProject.Domain.Authentication.Roles;

namespace OnionArchitectureProject.Authentication.Repositories.RoleRepository;
public class RoleRepository : IRoleRepository
{
    private readonly RoleManager<ApplicationRole> roleManager;
    private readonly IMapper mapper;

    public RoleRepository(RoleManager<ApplicationRole> roleManager, IMapper mapper)
    {
        this.roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<List<Role>> GetAllAsync()
    {
        var appRoles = await roleManager.Roles
            .OrderByDescending(r => r.CreatedUtcDate)
            .ToListAsync();
        return mapper.Map<List<Role>>(appRoles);
    }

    public async Task<Role?> GetByIdAsync(string id)
    {
        var appRole = await GetAppRoleByIdAsync(id);
        return appRole != null ? mapper.Map<Role>(appRole) : null;
    }

    public async Task<Role?> GetByNameAsync(string roleName)
    {
        var appRole = await GetAppRoleByNameAsync(roleName);
        return appRole != null ? mapper.Map<Role>(appRole) : null;
    }

    public async Task<Role?> GetByNameIncludingDeletedAsync(string roleName)
    {
        var appRole = await GetAppRoleIncludingDeletedByNameAsync(roleName);
        return appRole != null ? mapper.Map<Role>(appRole) : null;
    }

    public async Task<bool> ExistAsync(string id) =>
        await GetAppRoleByIdAsync(id) != null;

    public async Task<bool> RoleNameExistsAsync(string roleName) =>
        await roleManager.Roles.AnyAsync(r => r.Name == roleName);

    public async Task<Role> CreateAsync(Role role)
    {
        role.Id = Guid.NewGuid().ToString();
        var appRole = mapper.Map<ApplicationRole>(role);
        await roleManager.CreateAsync(appRole);
        return role;
    }

    public async Task UpdateAsync(Role role)
    {
        var existingAppRole = await GetAppRoleByIdAsync(role.Id);
        if (existingAppRole != null)
        {
            mapper.Map(role, existingAppRole);
            await roleManager.UpdateAsync(existingAppRole);
        }
    }

    public async Task DeleteAsync(string roleName)
    {
        var existingAppRole = await GetAppRoleByNameAsync(roleName);
        if (existingAppRole != null)
            await roleManager.DeleteAsync(existingAppRole);
    }

    public async Task RestoreAsync(string roleName)
    {
        var existingAppRole = await GetAppRoleIncludingDeletedByNameAsync(roleName);
        if (existingAppRole != null)
        {
            existingAppRole.IsDeleted = false;
            await roleManager.UpdateAsync(existingAppRole);
        }
    }

    private async Task<ApplicationRole?> GetAppRoleByIdAsync(string id) =>
        await roleManager.FindByIdAsync(id);

    private async Task<ApplicationRole?> GetAppRoleByNameAsync(string roleName) =>
        await roleManager.Roles
                .FirstOrDefaultAsync(r => r.Name == roleName);

    private async Task<ApplicationRole?> GetAppRoleIncludingDeletedByNameAsync(string roleName) =>
        await roleManager.Roles
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(r => r.Name == roleName);
}