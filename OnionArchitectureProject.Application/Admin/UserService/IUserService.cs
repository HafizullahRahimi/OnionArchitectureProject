using OnionArchitectureProject.Application.Admin.UserService.Models;
using OnionArchitectureProject.Application.Common.Models;

namespace OnionArchitectureProject.Application.Admin.UserService;
public interface IUserService
{
    Task<List<UserDto>> GetUsersAsync();
    Task<OperationResult> CreateAsync(CreateUserDto createUserDto);
}