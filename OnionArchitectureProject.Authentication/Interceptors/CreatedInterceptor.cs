using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using OnionArchitectureProject.Domain.Base;

namespace OnionArchitectureProject.Authentication.Interceptors;
public class CreatedInterceptor : SaveChangesInterceptor
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public CreatedInterceptor(IHttpContextAccessor httpContextAccessor)
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
        IEnumerable<EntityEntry<ICreated>> entries =
              eventData
              .Context
              .ChangeTracker.Entries<ICreated>()
              .Where(_ => _.State == Microsoft.EntityFrameworkCore.EntityState.Added);
        var currentUser = httpContextAccessor.HttpContext.User;
        var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "f37c60d7-47b8-4375-a226-77eaa6fe8885";
        foreach (EntityEntry<ICreated> softDeletable in entries)
        {
            softDeletable.State = Microsoft.EntityFrameworkCore.EntityState.Added;
            softDeletable.Entity.CreatedBy = currentUserId;
            softDeletable.Entity.CreatedDateUtc = DateTime.UtcNow;
        }
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}