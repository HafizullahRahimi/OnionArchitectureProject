using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OnionArchitectureProject.Domain.Base;
using System.Security.Claims;

namespace OnionArchitectureProject.Persistence.Interceptors;
public class ModifiedInterceptor : SaveChangesInterceptor
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public ModifiedInterceptor(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        IEnumerable<EntityEntry<IModified>> entries =
              eventData
              .Context
              .ChangeTracker.Entries<IModified>()
              .Where(_ => _.State == Microsoft.EntityFrameworkCore.EntityState.Modified);
        var currentUserId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Unknown";
        foreach (EntityEntry<IModified> softDeletable in entries)
        {
            softDeletable.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            softDeletable.Entity.ModifiedBy = currentUserId;
            softDeletable.Entity.ModifiedAt = DateTime.Now;
        }
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}