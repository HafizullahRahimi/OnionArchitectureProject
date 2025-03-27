using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OnionArchitectureProject.Application.Admin.RoleService;
using OnionArchitectureProject.Application.Admin.RoleService.Models;
using OnionArchitectureProject.Application.Admin.RoleService.Models.UpsertRoleDto;
using OnionArchitectureProject.Application.Common.Models;
using OnionArchitectureProject.Authentication.Models;
using OnionArchitectureProject.Domain.Authentication;

namespace OnionArchitectureProject.Authentication.Services.Admin.RoleService;

public class RoleService : IRoleService
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<RoleService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    public RoleService(
        RoleManager<ApplicationRole> roleManager,
        IUserRepository userRepository,
        IMapper mapper,
        ILogger<RoleService> logger,
        IServiceScopeFactory scopeFactory)
    {
        _roleManager = roleManager;
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    public async Task<List<RoleDto>> GetRolesAsync()
    {
        try
        {
            var appRoles = await _roleManager.Roles
                .OrderByDescending(r => r.CreatedUtcDate)
                .ToListAsync();

            var roleDtos = await Task.WhenAll(
                appRoles.Select(MapToRoleDtoAsync)
            );

            return roleDtos.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching roles");
            throw;
        }
    }

    public async Task<OperationResult> CreateAsync(UpsertRoleDto upsertRoleDto)
    {
        try
        {
            var (exists, message) = await ValidateRoleNameAsync(upsertRoleDto.Name);
            if (exists)
            {
                return new OperationResult(false, message);
            }

            var newAppRole = _mapper.Map<ApplicationRole>(upsertRoleDto);
            var result = await _roleManager.CreateAsync(newAppRole);

            return HandleIdentityResult(result, "create", upsertRoleDto.Name);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating role '{RoleName}'", upsertRoleDto.Name);
            throw;
        }
    }

    public async Task<OperationResult> UpdateAsync(string roleId, UpsertRoleDto upsertRoleDto)
    {
        try
        {
            var existingAppRole = await GetAppRoleByIdAsync(roleId);
            if (existingAppRole == null)
            {
                return new OperationResult(false, "Role not found.");
            }

            var (exists, message) = await ValidateRoleNameAsync(upsertRoleDto.Name, existingAppRole.Name);
            if (exists)
            {
                return new OperationResult(false, message);
            }

            _mapper.Map(upsertRoleDto, existingAppRole);
            var result = await _roleManager.UpdateAsync(existingAppRole);

            return HandleIdentityResult(result, "update", upsertRoleDto.Name);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating role '{RoleName}'", upsertRoleDto.Name);
            throw;
        }
    }

    public async Task<OperationResult> DeleteAsync(string roleId)
    {
        try
        {
            var existingAppRole = await GetAppRoleByIdAsync(roleId);
            if (existingAppRole == null)
            {
                return new OperationResult(false, "Role not found.");
            }

            var result = await _roleManager.DeleteAsync(existingAppRole);
            return HandleIdentityResult(result, "delete", existingAppRole.Name);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting role with ID: {RoleId}", roleId);
            throw;
        }
    }

    public UpsertRoleDto MapToUpsertRoleDto(RoleDto roleDto)
    {
        return _mapper.Map<UpsertRoleDto>(roleDto);
    }

    public async Task<bool> ExistsByRoleNameAsync(string roleName, CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        var exists = await roleManager.Roles
            .IgnoreQueryFilters()
            .AnyAsync(r => r.Name == roleName, cancellationToken);
        return exists;
    }

    private async Task<RoleDto> MapToRoleDtoAsync(ApplicationRole appRole)
    {
        var roleDto = _mapper.Map<RoleDto>(appRole);
        roleDto.CreatedByUserName = await GetUserNameByIdAsync(appRole.CreatedBy) ?? "Unknown";
        roleDto.ModifiedByUserName = await GetUserNameByIdAsync(appRole.ModifiedBy) ?? "Unknown";
        return roleDto;
    }

    private async Task<string?> GetUserNameByIdAsync(string userId) =>
        await _userRepository.GetUserNameByUserIdAsync(userId);

    private async Task<(bool exists, string message)> ValidateRoleNameAsync(string roleName, string? excludeRoleName = null)
    {
        var exists = await _roleManager.Roles
            .IgnoreQueryFilters()
            .AnyAsync(r => r.Name == roleName && (excludeRoleName == null || r.Name != excludeRoleName));

        return (exists, exists ? $"Role '{roleName}' already exists." : string.Empty);
    }

    private async Task<ApplicationRole?> GetAppRoleByIdAsync(string roleId)
    {
        return await _roleManager.FindByIdAsync(roleId);
    }

    private OperationResult HandleIdentityResult(IdentityResult result, string operation, string roleName)
    {
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            _logger.LogWarning("Failed to {Operation} role: {Errors}", operation, errors);
            return new OperationResult(false, $"Failed to {operation} role: {errors}");
        }

        _logger.LogInformation("Role '{RoleName}' {Operation}d successfully", roleName, operation);
        return new OperationResult(true, null);
    }
}