namespace OnionArchitectureProject.Domain.Authentication.Roles;
public interface IRoleRepository
{
    Task<List<Role>> GetAllAsync();
    Task<Role?> GetByIdAsync(string id);
    Task<bool> RoleNameExistsAsync(string roleName);
    Task<bool> ExistAsync(string id);
    Task<Role> CreateAsync(Role role);
    Task UpdateAsync(Role role);
    Task DeleteAsync(Role role);
    Task DeleteAsync(string id);
}