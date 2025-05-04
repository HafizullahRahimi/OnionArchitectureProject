using OnionArchitectureProject.Application.Admin.UserService.Models;
using OnionArchitectureProject.Application.Common.Models;
using OnionArchitectureProject.Domain.Authentication.Roles;

namespace OnionArchitectureProject.Application.Admin.UserService;
public interface IUserService
{
    Task<List<UserDto>> GetUsersAsync();
    Task<OperationResult> CreateAsync(UpsertUserDto upsertUserDto);
    Task<OperationResult> UpdateAsync(UpsertUserDto upsertUserDto);
    Task<OperationResult> DeleteAsync(string userId);
    UpsertUserDto MapToUpsertUserDto(UserDto userDto);
    Task<List<Role>?> GetAvailableRolesAsync();
    Task<UserRoleAssignmentDto> GetUserRolesAsync(string userId);
    Task<OperationResult> UpdateUserRolesAsync(UserRoleAssignmentDto userRoleAssignmentDto);
}