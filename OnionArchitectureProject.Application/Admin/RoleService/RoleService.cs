using AutoMapper;
using OnionArchitectureProject.Application.Admin.RoleService.Models;
using OnionArchitectureProject.Application.Admin.RoleService.Models.UpsertRoleDto;
using OnionArchitectureProject.Application.Common;
using OnionArchitectureProject.Application.Authentication.Roles;
using System.Data;
using OnionArchitectureProject.Domain.Authentication.Users;

namespace OnionArchitectureProject.Application.Admin.RoleService;
public class RoleService(IRoleRepository roleRepository, IMapper mapper, IUserRepository userRepository) : IRoleService
{
    private readonly IRoleRepository roleRepository = roleRepository;
    private readonly IMapper mapper = mapper;
    private readonly IUserRepository userRepository = userRepository;

    public async Task<List<RoleDto>> GetRolesAsync()
    {
        var roles = await roleRepository.GetAllAsync();
        var rolesWithUserName = new List<RoleDto>();
        foreach (var role in roles)
        {
            rolesWithUserName.Add(await MapToRoleDtoAsync(role));
        }
        return [.. rolesWithUserName.OrderBy(r => r.CreatedLocalTime)];
    }

    public async Task<OperationResult> CreateAsync(UpsertRoleDto upsertRoleDto)
    {
        try
        {
            var roleExists = await roleRepository.RoleNameExistsAsync(upsertRoleDto.Name);
            if (!roleExists)
            {
                var newRole = mapper.Map<Role>(upsertRoleDto);
                await roleRepository.CreateAsync(newRole);
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
            var existingRole = await roleRepository.GetByIdAsync(roleId);
            if (existingRole == null)
                return new OperationResult(false, "Role not found.");

            var roleNameExists = await roleRepository.RoleNameExistsAsync(upsertRoleDto.Name);
            if (roleNameExists && existingRole.Name != upsertRoleDto.Name)
                return new OperationResult(false, $"Role '{upsertRoleDto.Name}' already exists.");

            mapper.Map(upsertRoleDto, existingRole);
            await roleRepository.UpdateAsync(existingRole);
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
            var existingRole = await roleRepository.GetByIdAsync(roleId);
            if (existingRole != null)
            {
                await roleRepository.DeleteAsync(roleId);
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

    private async Task<RoleDto> MapToRoleDtoAsync(Role role)
    {
        var roleDto = mapper.Map<RoleDto>(role);
        roleDto.CreatedByUserName = await GetUserNameByIdAsync(role.CreatedBy) ?? roleDto.CreatedByUserName;
        roleDto.ModifiedByUserName = await GetUserNameByIdAsync(role.ModifiedBy) ?? roleDto.ModifiedByUserName;
        return roleDto;
    }

    private async Task<string?> GetUserNameByIdAsync(string userId) =>
        await userRepository.GetUserNemeByIdAsync(userId);
}