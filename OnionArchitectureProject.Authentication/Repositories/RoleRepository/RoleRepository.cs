using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnionArchitectureProject.Authentication.Models;
using OnionArchitectureProject.Domain.Authentication.Roles;

namespace OnionArchitectureProject.Authentication.Repositories.RoleRepository;
public class RoleRepository(RoleManager<ApplicationRole> roleManager, IMapper mapper) : IRoleRepository
{
    private readonly RoleManager<ApplicationRole> roleManager = roleManager;
    private readonly IMapper mapper = mapper;

    public async Task<List<Role>> GetAllAsync()
    {
        var appRoles = await roleManager.Roles
            .OrderByDescending(r => r.CreatedUtcDate)
            .ToListAsync();
        return mapper.Map<List<Role>>(appRoles);
    }

    public async Task<Role?> GetByIdAsync(string id)
    {
        var appRole = await roleManager.FindByIdAsync(id);
        return appRole != null ? mapper.Map<Role>(appRole) : null;
    }

    public async Task<bool> ExistAsync(string id)
    {
        var appRole = await GetAppRoleByIdAsync(id);
        return appRole != null;
    }
    public async Task<bool> RoleNameExistsAsync(string roleName)
    {
        var appRole = await roleManager.Roles
               .IgnoreQueryFilters()
               .FirstOrDefaultAsync(r => r.Name == roleName);
        return appRole != null;
    }

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

    public async Task DeleteAsync(Role role) =>
        await DeleteAsync(role.Id);

    public async Task DeleteAsync(string id)
    {
        var existingAppRole = await GetAppRoleByIdAsync(id);
        if (existingAppRole != null)
            await roleManager.DeleteAsync(existingAppRole);
    }

    private async Task<ApplicationRole?> GetAppRoleByIdAsync(string id) =>
        await roleManager.FindByIdAsync(id);
}