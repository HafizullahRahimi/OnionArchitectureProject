using OnionArchitectureProject.Domain.Base;

namespace OnionArchitectureProject.Domain.Authentication.Users;
public class User : EditableEntityBase<string>
{
    public string UserName { get; set; } = string.Empty;
    //public string NormalizedUserName { get; set; }
    public string Email { get; set; } = string.Empty;
    //public string NormalizedEmail { get; set; } 
    public bool EmailConfirmed { get; set; }

    //public string ConcurrencyStamp { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public bool PhoneNumberConfirmed { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; }
    public bool LockoutEnabled { get; set; }
    public int AccessFailedCount { get; set; }
}