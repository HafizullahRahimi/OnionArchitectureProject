using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnionArchitectureProject.Application.Admin.RoleService;
using OnionArchitectureProject.Application.Admin.RoleService.Models;
using OnionArchitectureProject.Application.Admin.RoleService.Models.UpsertRoleDto;
using OnionArchitectureProject.Application.Common.Models;
using OnionArchitectureProject.Authentication.Models;
using OnionArchitectureProject.Domain.Authentication;

namespace OnionArchitectureProject.Authentication.Services.RoleService;
public class RoleService(RoleManager<ApplicationRole> roleManager, IUserRepository authenticationRepository, IMapper mapper) : IRoleService
{
    private readonly RoleManager<ApplicationRole> roleManager = roleManager;
    private readonly IUserRepository authenticationRepository = authenticationRepository;
    private readonly IMapper mapper = mapper;

    public async Task<List<RoleDto>> GetRolesAsync()
    {
        var appRoles = await roleManager.Roles.OrderByDescending(r => r.CreatedUtcDate).ToListAsync();
        var rolesWithUserName = new List<RoleDto>();
        foreach (var appRole in appRoles)
        {
            rolesWithUserName.Add(await MapToRoleDtoAsync(appRole));
        }
        return rolesWithUserName;
    }

    public async Task<OperationResult> CreateAsync(UpsertRoleDto upsertRoleDto)
    {
        try
        {
            var roleExists = await RoleNameExistsAsync(upsertRoleDto.Name);
            if (!roleExists)
            {
                var newAppRole = mapper.Map<ApplicationRole>(upsertRoleDto);
                await roleManager.CreateAsync(newAppRole);
                return new OperationResult(true, null);
            }
            return new OperationResult(false, $"Role '{upsertRoleDto.Name}' already exists.");
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<OperationResult> UpdateAsync(string roleId, UpsertRoleDto upsertRoleDto)
    {
        try
        {
            var existingAppRole = await GetAppRoleByIdAsync(roleId);
            if (existingAppRole == null)
                return new OperationResult(false, "Role not found.");

            var roleNameExists = await RoleNameExistsAsync(upsertRoleDto.Name);
            if (roleNameExists && existingAppRole.Name != upsertRoleDto.Name)
                return new OperationResult(false, $"Role '{upsertRoleDto.Name}' already exists.");

            mapper.Map(upsertRoleDto, existingAppRole);
            await roleManager.UpdateAsync(existingAppRole);
            return new OperationResult(true, null);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<OperationResult> DeleteAsync(string roleId)
    {
        try
        {
            var existingAppRole = await GetAppRoleByIdAsync(roleId);
            if (existingAppRole != null)
            {
                await roleManager.DeleteAsync(existingAppRole);
                return new OperationResult(true, null);
            }
            return new OperationResult(false, "Role not found.");
        }
        catch (Exception)
        {
            throw;
        }
    }

    public UpsertRoleDto MapToUpsertRoleDto(RoleDto roleDto)
    {
        return mapper.Map<UpsertRoleDto>(roleDto);
    }

    private async Task<RoleDto> MapToRoleDtoAsync(ApplicationRole appRole)
    {
        var roleDto = mapper.Map<RoleDto>(appRole);
        roleDto.CreatedByUserName = await GetUserNameByIdAsync(appRole.CreatedBy) ?? "Unknown";
        roleDto.ModifiedByUserName = await GetUserNameByIdAsync(appRole.ModifiedBy) ?? "Unknown";
        return roleDto;
    }

    private async Task<string?> GetUserNameByIdAsync(string userId) =>
        await authenticationRepository.GetUserNemeByUserIdAsync(userId);

    private async Task<bool> RoleNameExistsAsync(string roleName)
    {
        var appRole = await roleManager.Roles
               .IgnoreQueryFilters()
               .FirstOrDefaultAsync(r => r.Name == roleName);
        return appRole != null;
    }

    private async Task<ApplicationRole?> GetAppRoleByIdAsync(string roleId)
    {
        var appRole = await roleManager.FindByIdAsync(roleId);
        return appRole;
    }
}