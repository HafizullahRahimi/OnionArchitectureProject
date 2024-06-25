using OnionArchitecture.Authentication.Services.AuthService.Models;

namespace OnionArchitecture.Authentication.Services.AuthService;
public interface IAuthService
{
    Task<AuthResponse?> Login(AuthRequest request);
    Task<RegistrationResponse> Register(RegisterationRequest request);
    AuthResponse? GetCurrentUserInfo(string token);
}