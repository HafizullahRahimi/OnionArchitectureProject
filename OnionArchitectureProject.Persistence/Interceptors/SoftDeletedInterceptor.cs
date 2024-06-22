using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OnionArchitecture.Domain.Common.BaseEntities;

namespace OnionArchitectureProject.Persistence.Interceptors;
public class SoftDeletedInterceptor : SaveChangesInterceptor
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

        IEnumerable<EntityEntry<ISoftDeleted>> entries =
            eventData
            .Context
            .ChangeTracker.Entries<ISoftDeleted>()
            .Where(_ => _.State == Microsoft.EntityFrameworkCore.EntityState.Deleted);

        foreach (EntityEntry<ISoftDeleted> softDeletable in entries)
        {
            softDeletable.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            softDeletable.Entity.IsDeleted = true;
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
