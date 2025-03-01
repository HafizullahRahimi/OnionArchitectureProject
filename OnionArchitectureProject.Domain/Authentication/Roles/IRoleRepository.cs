using OnionArchitectureProject.Domain.Base.Repositories;

namespace OnionArchitectureProject.Domain.Authentication.Roles;
public interface IRoleRepository : IAuthRepository<Role>
{
    Task<bool> RoleNameExistsAsync(string roleName);
}