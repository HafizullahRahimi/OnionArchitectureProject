using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace OnionArchitectureProject.Persistence.Extensions;
internal static class JwtTokenExtension
{
    public static JwtSecurityToken? GetJwtToken(this IHttpContextAccessor httpContextAccessor)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext == null)
            return null;

        var token = httpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", string.Empty);
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
        return jwtToken;
    }

    public static string GetCurrentUserId(this JwtSecurityToken jwtSecurityToken) =>
        GetClaimsByType(jwtSecurityToken, "uid");

    public static string GetCurrentUserName(this JwtSecurityToken jwtSecurityToken) =>
        GetClaimsByType(jwtSecurityToken, "sub");

    public static string GetCurrentUserEmail(this JwtSecurityToken jwtSecurityToken) =>
        GetClaimsByType(jwtSecurityToken, "email");

    private static string GetClaimsByType(this JwtSecurityToken jwtSecurityToken, string type) =>
        (jwtSecurityToken != null) ?
        jwtSecurityToken.Claims.First(claim => claim.Type == type).Value :
        string.Empty;
}
