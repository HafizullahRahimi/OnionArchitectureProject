using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace OnionArchitectureProject.Authentication.Services.CurrentUserService;
public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetId() => GetClaimsByType("uid");

    public string GetName() => GetClaimsByType("sub");

    public string GetEmail() => GetClaimsByType("email");

    private string GetClaimsByType(string type)
    {
        var jwtToken = GetJwtToken();
        return jwtToken != null ?
            jwtToken.Claims.First(claim => claim.Type == type).Value :
            string.Empty;
    }

    private JwtSecurityToken? GetJwtToken()
    {
        var httpContext = GetHttpContext();
        if (httpContext != null)
        {
            var token = httpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", string.Empty);
            if (token != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
                return jwtToken;
            }
        }

        return null;
    }

    private HttpContext? GetHttpContext() =>
        _httpContextAccessor.HttpContext != null ?
        _httpContextAccessor.HttpContext :
        null;
}