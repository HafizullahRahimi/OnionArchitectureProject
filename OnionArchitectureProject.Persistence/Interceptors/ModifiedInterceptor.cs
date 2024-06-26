using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OnionArchitectureProject.Domain.Common.BaseEntities;

namespace OnionArchitectureProject.Persistence.Interceptors;
public class ModifiedInterceptor : SaveChangesInterceptor
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

        IEnumerable<EntityEntry<IModified>> entries =
              eventData
              .Context
              .ChangeTracker.Entries<IModified>()
              .Where(_ => _.State == Microsoft.EntityFrameworkCore.EntityState.Modified);

        foreach (EntityEntry<IModified> softDeletable in entries)
        {
            softDeletable.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            softDeletable.Entity.ModifiedBy = "ModifiedByHafizullah";
            softDeletable.Entity.ModifiedDateUTC = DateTime.Now;
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}