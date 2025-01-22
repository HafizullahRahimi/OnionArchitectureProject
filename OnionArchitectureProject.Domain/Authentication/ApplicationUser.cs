using Microsoft.AspNetCore.Identity;
using OnionArchitectureProject.Domain.Base;

namespace OnionArchitectureProject.Domain.Authentication;
public class ApplicationUser : IdentityUser, ICreated, IModified, ISoftDeleted
{
    public DateTime CreatedDateUtc { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime ModifiedDateUtc { get; set; }
    public string ModifiedBy { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
}