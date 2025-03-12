using OnionArchitectureProject.Application.Admin.UserService.Models;

namespace OnionArchitectureProject.Application.Admin.UserService;
public interface IUserService
{
    Task<List<UserDto>> GetUsersAsync();
}