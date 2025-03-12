using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OnionArchitectureProject.Domain.Base;

namespace OnionArchitectureProject.Authentication.Interceptors;

public class CreatedInterceptor : BaseInterceptor
{
    public CreatedInterceptor(IHttpContextAccessor httpContextAccessor)
        : base(httpContextAccessor)
    {
    }

    protected override void ProcessEntities(DbContextEventData eventData)
    {
        var entries = eventData.Context!.ChangeTracker
            .Entries<ICreatedEntity>()
            .Where(e => e.State == EntityState.Added);

        foreach (var entry in entries)
        {
            entry.Entity.CreatedBy = CurrentUserId;
            entry.Entity.CreatedUtcDate = DateTime.UtcNow;
        }
    }
}