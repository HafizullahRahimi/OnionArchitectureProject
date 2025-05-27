using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OnionArchitectureProject.Application.Admin.RoleService.Models;
using OnionArchitectureProject.Application.Common.Models;
using OnionArchitectureProject.Domain.Authentication.Roles;
using OnionArchitectureProject.Domain.Authentication.Users;

namespace OnionArchitectureProject.Application.Admin.RoleService;
public class RoleService : IRoleService
{
    private readonly IRoleRepository roleRepository;
    private readonly IMapper mapper;
    private readonly IServiceScopeFactory scopeFactory;
    private readonly ILogger<RoleService> logger;

    public RoleService(
        IRoleRepository roleRepository,
        IMapper mapper,
        IServiceScopeFactory scopeFactory,
        ILogger<RoleService> logger)
    {
        this.roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this.scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<List<RoleDto>> GetRolesAsync()
    {
        var roles = await roleRepository.GetAllAsync();
        var roleDtos = await Task.WhenAll(
            roles.Select(MapToRoleDtoAsync)
        );
        return roleDtos.ToList();
    }

    public async Task<OperationResult> CreateAsync(UpsertRoleDto upsertRoleDto)
    {
        try
        {
            var roleCheckResult = await CheckIfRoleExistsAndIsDeleted(upsertRoleDto.Name);
            if (!roleCheckResult.Succeeded)
                return roleCheckResult;

            var newRole = mapper.Map<Role>(upsertRoleDto);
            await roleRepository.CreateAsync(newRole);
            return new OperationResult(true, null);
        }
        catch (Exception ex)
        {
            logger.LogError($"Error occurred while creating role {upsertRoleDto.Name}: {ex}");
            return new OperationResult(false, "An error occurred while creating the role. Please try again later.");
        }
    }

    public async Task<OperationResult> UpdateAsync(UpsertRoleDto upsertRoleDto)
    {
        try
        {
            var existingRole = await ValidateAndGetExistingRoleAsync(upsertRoleDto.Id);
            if (existingRole == null)
                return new OperationResult(false, "Role not found.");

            var roleCheckResult = await CheckIfRoleExistsAndIsDeleted(upsertRoleDto.Name);
            if (!roleCheckResult.Succeeded)
                return roleCheckResult;

            mapper.Map(upsertRoleDto, existingRole);
            await roleRepository.UpdateAsync(existingRole);
            return new OperationResult(true, null);

        }
        catch (Exception ex)
        {
            logger.LogError($"Error occurred while updating role {upsertRoleDto?.Id}: {ex}");
            return new OperationResult(false, "An error occurred while updating the role. Please try again later.");
        }
    }

    public async Task<OperationResult> DeleteAsync(string roleName)
    {
        try
        {
            var existingRole = await roleRepository.GetByNameAsync(roleName);
            if (existingRole == null)
                return new OperationResult(false, "Role not found.");

            await roleRepository.DeleteAsync(roleName);
            return new OperationResult(true, null);
        }
        catch (Exception ex)
        {
            logger.LogError($"Error occurred while deleting role {roleName}: {ex}");
            return new OperationResult(false, "An error occurred while deleting the role. Please try again later.");
        }
    }

    public async Task<OperationResult> RestoreAsync(string roleName)
    {
        try
        {
            var existingRole = await roleRepository.GetByNameIncludingDeletedAsync(roleName);
            if (existingRole == null)
                return new OperationResult(false, "Role not found.");

            if (!existingRole.IsDeleted)
                return new OperationResult(false, "Role is not deleted.");

            await roleRepository.RestoreAsync(roleName);
            return new OperationResult(true, null);
        }
        catch (Exception ex)
        {
            logger.LogError($"Error occurred while restoring role {roleName}: {ex}");
            return new OperationResult(false, "An error occurred while restoring the role. Please try again later.");
        }
    }

    public UpsertRoleDto MapToUpsertRoleDto(RoleDto roleDto)
    {
        return mapper.Map<UpsertRoleDto>(roleDto);
    }

    private async Task<RoleDto> MapToRoleDtoAsync(Role role)
    {
        var roleDto = mapper.Map<RoleDto>(role);
        roleDto.CreatedByUserName = await GetUserNameByIdAsync(role.CreatedBy) ?? "Unknown";
        roleDto.ModifiedByUserName = await GetUserNameByIdAsync(role.ModifiedBy) ?? "Unknown";
        return roleDto;
    }

    private async Task<string?> GetUserNameByIdAsync(string userId)
    {
        using var scope = scopeFactory.CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
        var user = await userRepository.GetByIdAsync(userId);
        return user?.UserName;
    }

    private async Task<OperationResult> CheckIfRoleExistsAndIsDeleted(string roleName)
    {
        var existingRole = await roleRepository.GetByNameIncludingDeletedAsync(roleName);
        if (existingRole?.IsDeleted == true)
            return new OperationResult(false, "RoleDeleted");

        return new OperationResult(true, null);
    }

    private async Task<Role?> ValidateAndGetExistingRoleAsync(string? roleId)
    {
        if (string.IsNullOrEmpty(roleId))
            return null;
        return await roleRepository.GetByIdAsync(roleId);
    }
}