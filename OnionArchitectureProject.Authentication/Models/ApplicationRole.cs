using Microsoft.AspNetCore.Identity;
using OnionArchitectureProject.Domain.Base;

namespace OnionArchitectureProject.Authentication.Models;
public class ApplicationRole : IdentityRole, ICreatedEntity, IModifiedEntity, ISoftDeletableEntity
{
    public string? Description { get; set; }
    public DateTime CreatedUtcDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime ModifiedUtcDate { get; set; }
    public string ModifiedBy { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
}