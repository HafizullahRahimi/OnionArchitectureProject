using OnionArchitectureProject.Authentication.Services.AuthService.Models;

namespace OnionArchitectureProject.Authentication.Services.AuthService;
public interface IAuthService
{
    Task<AuthResponse?> Login(AuthRequest request);
    Task<RegistrationResponse> Register(RegisterationRequest request);
}