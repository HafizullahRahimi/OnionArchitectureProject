using OnionArchitectureProject.Application.Admin.RoleService.Models;
using OnionArchitectureProject.Application.Admin.RoleService.Models.UpsertRoleDto;
using OnionArchitectureProject.Application.Common;

namespace OnionArchitectureProject.Application.Admin.RoleService;
public interface IRoleService
{
    Task<List<RoleDto>> GetRolesAsync();
    Task<OperationResult> CreateAsync(UpsertRoleDto upsertRoleDto);
    Task<OperationResult> UpdateAsync(string roleId, UpsertRoleDto upsertRoleDto);
    Task<OperationResult> DeleteAsync(string roleId);
    UpsertRoleDto MapToUpsertRoleDto(RoleDto roleDto);
}