using Microsoft.AspNetCore.Builder;

namespace OnionArchitectureProject.Authentication.SeedData;

public static class SeedUsersAndRolesMiddlewareExtensions
{
    public static IApplicationBuilder UseSeedUsersAndRoles(this IApplicationBuilder app)
    {
        return app.UseMiddleware<SeedUsersAndRolesMiddleware>();
    }
}