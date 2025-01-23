using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OnionArchitectureProject.Application.Admin.ApplicationRoleService.Models;
using OnionArchitectureProject.Application.Admin.ApplicationRoleService.Models.UpsertRoleDto;
using OnionArchitectureProject.Domain.Authentication;

namespace OnionArchitectureProject.Application.Admin.ApplicationRoleService;
public class ApplicationRoleService : IApplicationRoleService
{
    private readonly IMapper mapper;
    private readonly RoleManager<ApplicationRole> roleManager;
    private readonly UserManager<ApplicationUser> userManager;

    public ApplicationRoleService(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        this.mapper = mapper;
        this.roleManager = roleManager;
        this.userManager = userManager;
    }

    public async Task<List<RoleDto>> GetRolesAsync()
    {
        var roles = roleManager.Roles.OrderBy(r => r.CreatedDateUtc);
        var rolesWithUserName = new List<RoleDto>();
        foreach (var role in roles)
        {
            rolesWithUserName.Add(await MapToRoleDtoAsync(role));
        }
        return rolesWithUserName;
    }

    public async Task<bool> CreateAsync(UpsertRoleDto upsertRoleDto)
    {
        try
        {
            if (upsertRoleDto == null)
                return false;
            var role = mapper.Map<ApplicationRole>(upsertRoleDto);
            var result = await roleManager.CreateAsync(role);
            return result.Succeeded;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> UpdateAsync(string roleId, UpsertRoleDto upsertRoleDto)
    {
        try
        {
            if (upsertRoleDto == null)
                return false;
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
                return false;
            mapper.Map(upsertRoleDto, role);
            var result = await roleManager.UpdateAsync(role);
            return result.Succeeded;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> DeleteAsync(string roleId)
    {
        try
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
                return false;
            var result = await roleManager.DeleteAsync(role);
            return result.Succeeded;
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

    private async Task<RoleDto> MapToRoleDtoAsync(ApplicationRole role)
    {
        var roleDto = mapper.Map<RoleDto>(role);
        var createdByUserName = await GetUserNameByIdAsync(role.CreatedBy);
        if (createdByUserName != null)
        {
            roleDto.CreatedByUserName = createdByUserName;
        }
        if (!string.IsNullOrEmpty(role.ModifiedBy))
        {
            roleDto.ModifiedByUserName = await GetUserNameByIdAsync(role.ModifiedBy);
        }
        return roleDto;
    }

    private async Task<string?> GetUserNameByIdAsync(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return null;
        var user = await userManager.FindByIdAsync(userId);
        return user?.UserName ?? null;
    }
}