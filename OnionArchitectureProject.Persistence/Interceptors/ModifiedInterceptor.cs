using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OnionArchitectureProject.Domain.Base;
using System.Security.Claims;

namespace OnionArchitectureProject.Persistence.Interceptors;

public class ModifiedInterceptor : BaseInterceptor
{
    public ModifiedInterceptor(IHttpContextAccessor httpContextAccessor)
        : base(httpContextAccessor)
    {
    }

    protected override void ProcessEntities(DbContextEventData eventData)
    {
        var entries = eventData.Context!.ChangeTracker
            .Entries<IModifiedEntity>()
            .Where(e => e.State == EntityState.Modified);

        var currentUserId = httpContextAccessor?.HttpContext?.User
            .FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Unknown";

        foreach (var entry in entries)
        {
            entry.Entity.ModifiedBy = currentUserId;
            entry.Entity.ModifiedUtcDate = DateTime.UtcNow;
        }
    }
}