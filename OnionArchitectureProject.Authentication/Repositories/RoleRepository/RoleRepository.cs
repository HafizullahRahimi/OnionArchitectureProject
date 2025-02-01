using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnionArchitectureProject.Application.Authentication.Roles;
using OnionArchitectureProject.Authentication.Models;

namespace OnionArchitectureProject.Authentication.Repositories.RoleRepository;
public class RoleRepository(RoleManager<ApplicationRole> roleManager, IMapper mapper) : IRoleRepository
{
    private readonly RoleManager<ApplicationRole> roleManager = roleManager;
    private readonly IMapper mapper = mapper;

    public async Task<List<Role>> GetAllAsync()
    {
        var appRoles = await roleManager.Roles.ToListAsync();
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

    public async Task<Role> CreateAsync(Role entity)
    {
        entity.Id = Guid.NewGuid().ToString();
        var appRole = mapper.Map<ApplicationRole>(entity);
        await roleManager.CreateAsync(appRole);
        return entity;
    }

    public async Task UpdateAsync(Role entity)
    {
        var existingAppRole = await GetAppRoleByIdAsync(entity.Id);
        if (existingAppRole != null)
        {
            mapper.Map(entity, existingAppRole);
            await roleManager.UpdateAsync(existingAppRole);
        }
    }

    public async Task DeleteAsync(Role entity) =>
        await DeleteAsync(entity.Id);

    public async Task DeleteAsync(string id)
    {
        var existingAppRole = await GetAppRoleByIdAsync(id);
        if (existingAppRole != null)
            await roleManager.DeleteAsync(existingAppRole);
    }

    private async Task<ApplicationRole?> GetAppRoleByIdAsync(string id) =>
        await roleManager.FindByIdAsync(id);
}