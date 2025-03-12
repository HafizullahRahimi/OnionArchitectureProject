namespace OnionArchitectureProject.Application.Admin.UserService.Models;

public class UserDto
{
    public string Id { get; set; }
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

    public string CreatedByUserName { get; set; } = string.Empty;
    public DateTime CreatedLocalTime { get; set; }
    public string ModifiedByUserName { get; set; } = string.Empty;
    public DateTime ModifiedLocalTime { get; set; }
}