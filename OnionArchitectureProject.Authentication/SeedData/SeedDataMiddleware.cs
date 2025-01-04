using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OnionArchitectureProject.Authentication.Models;

namespace OnionArchitectureProject.Authentication.SeedData;
public class SeedDataMiddleware
{
    private readonly RequestDelegate _next;

    public SeedDataMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var services = scope.ServiceProvider;
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var users = userManager.Users.ToList();
            var roles = roleManager.Roles.ToList();

            if (!users.Any() || !roles.Any())
            {
                Console.WriteLine("Starting seed...");
                await SeedData.InitializeAsync(roleManager, userManager);
            }
            else
            {
                Console.WriteLine("Data already exists, skipping seeding.");
            }
        }
        await _next(context);
    }
}