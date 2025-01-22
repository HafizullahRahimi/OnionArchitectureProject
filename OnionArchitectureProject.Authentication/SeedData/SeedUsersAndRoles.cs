using Microsoft.AspNetCore.Identity;
using OnionArchitectureProject.Domain.Authentication;

namespace OnionArchitectureProject.Authentication.SeedData;
public static class SeedUsersAndRoles
{
    public static async Task InitializeAsync(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        var roles = new[] { "System", "User", "Admin" };
        await roleManager.SeedRoles(roles);
        var systemUserCreated = await userManager.SeedUserWithRole("System", "system@email.com", "System123!", "System");

    }

    private static async Task SeedRoles(this RoleManager<ApplicationRole> roleManager, string[] roles)
    {
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                Console.WriteLine($"Creating role: {role}");
                await roleManager.CreateAsync(new ApplicationRole()
                {
                    Name = role
                });
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
                Id = "f37c60d7-47b8-4375-a226-77eaa6fe8885",
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