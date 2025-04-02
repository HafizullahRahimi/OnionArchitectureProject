using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using OnionArchitectureProject.Application.Admin.RoleService.Models;
using OnionArchitectureProject.Application.Common.Models;
using OnionArchitectureProject.Domain.Authentication.Roles;
using OnionArchitectureProject.Domain.Authentication.Users;

namespace OnionArchitectureProject.Application.Admin.RoleService;
public class RoleService(IRoleRepository roleRepository, IMapper mapper, IServiceScopeFactory scopeFactory) : IRoleService
{
    private readonly IRoleRepository roleRepository = roleRepository;
    private readonly IMapper mapper = mapper;
    private readonly IServiceScopeFactory scopeFactory = scopeFactory;

    public async Task<List<RoleDto>> GetRolesAsync()
    {
        var roles = await roleRepository.GetAllAsync();
        var rolesWithUserName = new List<RoleDto>();
        foreach (var role in roles)
        {
            rolesWithUserName.Add(await MapToRoleDtoAsync(role));
        }
        return rolesWithUserName;
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

    public async Task<OperationResult> UpdateAsync(UpsertRoleDto upsertRoleDto)
    {
        try
        {
            if (string.IsNullOrEmpty(upsertRoleDto.Id))
                return new OperationResult(false, "Role ID is required.");

            var existingRole = await roleRepository.GetByIdAsync(upsertRoleDto.Id);
            if (existingRole == null)
                return new OperationResult(false, "Role not found.");

            mapper.Map(upsertRoleDto, existingRole);
            await roleRepository.UpdateAsync(existingRole);

            return new OperationResult(true, null);
        }
        catch (Exception ex)
        {
            return new OperationResult(false, $"An error occurred while updating the role: {ex.Message}");
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

    private async Task<string?> GetUserNameByIdAsync(string userId)
    {
        using var scope = scopeFactory.CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
        var user = await userRepository.GetByIdAsync(userId);
        return user?.UserName;
    }
}