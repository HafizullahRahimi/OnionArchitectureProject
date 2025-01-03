using Microsoft.AspNetCore.Identity;
using OnionArchitectureProject.Authentication.Models;

namespace OnionArchitectureProject.Web.SeedData;
public static class SeedData
{
    public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        var roles = new[] { "System", "Admin", "User" };
        await roleManager.SeedRoles(roles);
        var systemUserCreated = await userManager.SeedUserWithRole("@System", "system@email.com", "System123!", "System");

    }

    private static async Task SeedRoles(this RoleManager<IdentityRole> roleManager, string[] roles)
    {
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                Console.WriteLine($"Creating role: {role}");
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    private static async Task<bool> SeedUserWithRole(this UserManager<ApplicationUser> userManager, string userName, string email, string password, string role)
    {
        if (await userManager.FindByEmailAsync(email) == null)
        {
            Console.WriteLine($"Creating system user: {email}");
            var newUser = new ApplicationUser
            {
                UserName = userName,
                Email = email,
                EmailConfirmed = true
            };

            var createNewUserResult = await userManager.CreateAsync(newUser, password);
            if (createNewUserResult.Succeeded)
            {
                Console.WriteLine($"Adding user {email} to System role");
                await userManager.AddToRoleAsync(newUser, role);
                return true;
            }
            else
            {
                Console.WriteLine($"Failed to create system user: {string.Join(", ", createNewUserResult.Errors.Select(e => e.Description))}");
                return false;
            }
        }
        return false;
    }

}