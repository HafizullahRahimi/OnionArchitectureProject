using OnionArchitectureProject.Application.Admin.ApplicationRoleService.Models;
using OnionArchitectureProject.Application.Admin.ApplicationRoleService.Models.UpsertRoleDto;

namespace OnionArchitectureProject.Application.Admin.ApplicationRoleService;
public interface IApplicationRoleService
{
    Task<List<RoleDto>> GetRolesAsync();
    Task<bool> CreateAsync(UpsertRoleDto upsertRoleDto);
    Task<bool> UpdateAsync(string roleId, UpsertRoleDto upsertRoleDto);
    Task<bool> DeleteAsync(string roleId);
    UpsertRoleDto MapToUpsertRoleDto(RoleDto roleDto);
}