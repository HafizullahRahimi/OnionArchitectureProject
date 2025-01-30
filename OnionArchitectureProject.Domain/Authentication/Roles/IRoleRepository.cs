using OnionArchitectureProject.Domain.Base.Repositories;

namespace OnionArchitectureProject.Application.Authentication.Roles;
public interface IRoleRepository : IAuthRepository<Role>
{
    Task<bool> RoleNameExistsAsync(string roleName);
}