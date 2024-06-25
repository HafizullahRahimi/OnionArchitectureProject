using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnionArchitecture.Authentication.Models;

namespace OnionArchitecture.Authentication;
public class AuthenticationDbContext : IdentityDbContext<ApplicationUser>
{
    public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AuthenticationDbContext).Assembly);
    }
}