using Microsoft.AspNetCore.Builder;

namespace OnionArchitectureProject.Web.SeedData;

public static class SeedDataMiddlewareExtensions
{
    public static IApplicationBuilder UseSeedData(this IApplicationBuilder app)
    {
        return app.UseMiddleware<SeedDataMiddleware>();
    }
}