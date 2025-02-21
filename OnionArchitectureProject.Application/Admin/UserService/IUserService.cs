using OnionArchitectureProject.Domain.Authentication.Users;

namespace OnionArchitectureProject.Application.Admin.UserService;
public interface IUserService
{
    Task<List<User>> GetUsersAsync();
}
