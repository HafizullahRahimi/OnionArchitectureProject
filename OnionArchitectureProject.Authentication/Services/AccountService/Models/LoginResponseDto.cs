using OnionArchitectureProject.Authentication.Models;

namespace OnionArchitectureProject.Authentication.Services.AccountService.Models;
public record LoginResponseDto(bool Flag, string Message = null, ApplicationUser User = null);