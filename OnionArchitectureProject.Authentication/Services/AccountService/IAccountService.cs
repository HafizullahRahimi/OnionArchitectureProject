using OnionArchitectureProject.Authentication.Models;
using OnionArchitectureProject.Authentication.Services.AccountService.Models;

namespace OnionArchitectureProject.Authentication.Services.AccountService;
public interface IAccountService
{
    Task<ResponseDto> RegisterUserAsync(RegisterDto registerDto);
}