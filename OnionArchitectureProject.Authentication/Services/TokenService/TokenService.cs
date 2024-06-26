using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace OnionArchitectureProject.Authentication.Services.TokenService;
public class TokenService : ITokenService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetCurrentUserEmail()
    {
        var jwtToken = GetJwtToken();

        if (jwtToken == null)
            return null;

        var email = jwtToken.Claims.First(claim => claim.Type == "email").Value;
        return email;
    }

    public string? GetCurrentUserId()
    {
        var jwtToken = GetJwtToken();

        if (jwtToken == null)
            return null;

        var userId = jwtToken.Claims.First(claim => claim.Type == "uid").Value;
        return userId;
    }

    public string? GetCurrentUserName()
    {
        var jwtToken = GetJwtToken();

        if (jwtToken == null)
            return null;

        var userName = jwtToken.Claims.First(claim => claim.Type == "sub").Value;
        return userName;
    }


    private JwtSecurityToken? GetJwtToken()
    {
        var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
        if (jwtToken == null)
            return null;
        return jwtToken;
    }
}