using Microsoft.AspNetCore.Http;

namespace OnionArchitectureProject.Persistence.Extensions;
internal static class HttpContextAccessorExtension
{
    public static string GetClaimsByType(this IHttpContextAccessor httpContextAccessor, string claimsType)
    {
        return "User id";
    }
}