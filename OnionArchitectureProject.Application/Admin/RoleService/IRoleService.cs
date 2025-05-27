using OnionArchitectureProject.Application.Admin.RoleService.Models;
using OnionArchitectureProject.Application.Common.Models;

namespace OnionArchitectureProject.Application.Admin.RoleService;
public interface IRoleService
{
    Task<List<RoleDto>> GetRolesAsync();
    Task<OperationResult> CreateAsync(UpsertRoleDto upsertRoleDto);
    Task<OperationResult> UpdateAsync(UpsertRoleDto upsertRoleDto);
    Task<OperationResult> DeleteAsync(string roleName);
    Task<OperationResult> RestoreAsync(string roleName);
    UpsertRoleDto MapToUpsertRoleDto(RoleDto roleDto);
}