using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionArchitectureProject.Domain.Authentication.ApplicationRole;

namespace OnionArchitectureProject.Authentication.Configurations;
public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        //builder.HasData(
        //    new IdentityRole
        //    {
        //        Id = "9845f909-799c-45fd-9158-58c1336ffddc",
        //        Name = "User",
        //        NormalizedName = "USER"
        //    },
        //    new IdentityRole
        //    {
        //        Id = "cb275765-1cac-4652-a03f-f8871dd575d1",
        //        Name = "Admin",
        //        NormalizedName = "ADMIN"
        //    }
        //);
    }
}