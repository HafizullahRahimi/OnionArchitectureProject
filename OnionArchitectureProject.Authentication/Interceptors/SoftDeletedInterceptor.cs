using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OnionArchitectureProject.Domain.Base;

namespace OnionArchitectureProject.Authentication.Interceptors;

public class SoftDeletedInterceptor : BaseInterceptor
{
    protected override void ProcessEntities(DbContextEventData eventData)
    {
        var entries = eventData.Context!.ChangeTracker
            .Entries<ISoftDeletableEntity>()
            .Where(e => e.State == EntityState.Deleted);

        foreach (var entry in entries)
        {
            entry.State = EntityState.Modified;
            entry.Entity.IsDeleted = true;
        }
    }
}