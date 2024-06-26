namespace OnionArchitectureProject.Authentication.Services.TokenService;
public interface ITokenService
{
    string? GetCurrentUserId();
    string? GetCurrentUserName();
    string? GetCurrentUserEmail();
}