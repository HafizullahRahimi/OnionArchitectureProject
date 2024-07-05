using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OnionArchitectureProject.Domain.Common.BaseEntities;

namespace OnionArchitectureProject.Persistence.Interceptors;
public class CreatedInterceptor : SaveChangesInterceptor
{
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

        var currentUserId = "Created by user id";

        foreach (EntityEntry<ICreated> softDeletable in entries)
        {
            softDeletable.State = Microsoft.EntityFrameworkCore.EntityState.Added;
            softDeletable.Entity.CreatedBy = currentUserId;
            softDeletable.Entity.CreatedAt = DateTime.Now;
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}