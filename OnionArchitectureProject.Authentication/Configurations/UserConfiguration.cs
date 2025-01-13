//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using OnionArchitectureProject.Domain.Authentication.ApplicationUsers;

//namespace OnionArchitectureProject.Authentication.Configurations;
//public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
//{
//    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
//    {
//        //var hasher = new PasswordHasher<ApplicationUser>();

//        //builder.HasData(
//        //    new ApplicationUser
//        //    {
//        //        Id = "05446344-f9cc-4566-bd2c-36791b4e28ed",
//        //        Email = "admin@localhost.com",
//        //        NormalizedEmail = "ADMIN@LOCALHOST.COM",
//        //        FirstName = "Admin",
//        //        LastName = "System",
//        //        UserName = "Admin10",
//        //        NormalizedUserName = "ADMIN10",
//        //        PasswordHash = hasher.HashPassword(null, "P@ssword1"),
//        //        EmailConfirmed = true,
//        //    },
//        //    new ApplicationUser
//        //    {
//        //        Id = "2ec9f480-7288-4d0f-a1cd-53cc89968b45",
//        //        Email = "user@localhost.com",
//        //        NormalizedEmail = "USER@LOCALHOST.COM",
//        //        FirstName = "User",
//        //        LastName = "System",
//        //        UserName = "User20",
//        //        NormalizedUserName = "USER20",
//        //        PasswordHash = hasher.HashPassword(null, "P@ssword1"),
//        //        EmailConfirmed = true,
//        //    }
//        //);
//    }
//}