using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnionArchitectureProject.Authentication.Models;

namespace OnionArchitectureProject.Authentication;
public class AuthenticationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AuthenticationDbContext).Assembly);
    }
}