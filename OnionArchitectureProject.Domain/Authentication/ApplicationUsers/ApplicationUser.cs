using Microsoft.AspNetCore.Identity;
using OnionArchitectureProject.Domain.Base;

namespace OnionArchitectureProject.Domain.Authentication.ApplicationUsers;

public class ApplicationUser : IdentityUser, IEntityBase<string>
{
    //public string FirstName { get; set; } = string.Empty;
    //public string LastName { get; set; } = string.Empty;
}