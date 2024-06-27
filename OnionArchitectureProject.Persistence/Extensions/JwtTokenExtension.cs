using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace OnionArchitectureProject.Persistence.Extensions;
internal static class JwtTokenExtension
{

    public static string GetClaimsByType(this IHttpContextAccessor httpContextAccessor, string claimsType)
    {
        var token = httpContextAccessor.GetJwtToken();
        if (token != null) return token.GetClaimsByType(claimsType);
        return string.Empty;
    }

    private static JwtSecurityToken? GetJwtToken(this IHttpContextAccessor httpContextAccessor)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext == null) return null;
        var token = httpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", string.Empty);
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
        return jwtToken;
    }

    private static string GetClaimsByType(this JwtSecurityToken jwtSecurityToken, string type) =>
        (jwtSecurityToken != null) ?
        jwtSecurityToken.Claims.First(claim => claim.Type == type).Value :
        string.Empty;
}
