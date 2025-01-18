using Microsoft.AspNetCore.Identity;
using OnionArchitectureProject.Domain.Base;

namespace OnionArchitectureProject.Domain.Authentication.ApplicationRoles;
public class ApplicationRole : IdentityRole, ICreated, IModified, ISoftDeleted
{
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedBy { get; set; }
    public bool IsDeleted { get; set; }
}