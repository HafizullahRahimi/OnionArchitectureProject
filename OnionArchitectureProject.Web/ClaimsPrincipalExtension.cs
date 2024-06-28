using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace OnionArchitectureProject.Web;

public static class ClaimsPrincipalExtension
{
    public static string? GetName(this ClaimsPrincipal user) => GetClaimByType(user, "sub");
    public static string? GetId(this ClaimsPrincipal user) => GetClaimByType(user, "uid");
    public static string? GetEmail(this ClaimsPrincipal user) => GetClaimByType(user, "email");

    private static string? GetClaimByType(this ClaimsPrincipal user, string claimType)
    {
        return user.Claims.FirstOrDefault(c => c.Type == claimType)?.Value.ToString();
    }
}
