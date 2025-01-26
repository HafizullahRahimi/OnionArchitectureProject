using Microsoft.AspNetCore.Identity;
using OnionArchitectureProject.Domain.Base;

namespace OnionArchitectureProject.Authentication.Models;
public class ApplicationRole : IdentityRole, ICreated, IModified, ISoftDeleted
{
    public string? Description { get; set; }
    public DateTime CreatedDateUtc { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime ModifiedDateUtc { get; set; }
    public string ModifiedBy { get; set; } = String.Empty;
    public bool IsDeleted { get; set; }
}