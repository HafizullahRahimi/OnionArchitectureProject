namespace OnionArchitectureProject.Domain.Authentication.Roles;
public interface IRoleRepository
{
    Task<List<Role>> GetAllAsync();
    Task<Role?> GetByIdAsync(string id);
    Task<Role?> GetByNameAsync(string roleName);
    Task<Role?> GetByNameIncludingDeletedAsync(string roleName);
    Task<bool> ExistAsync(string id);
    Task<bool> RoleNameExistsAsync(string roleName);
    Task<Role> CreateAsync(Role role);
    Task UpdateAsync(Role role);
    Task DeleteAsync(string roleName);
    Task RestoreAsync(string roleName);
}