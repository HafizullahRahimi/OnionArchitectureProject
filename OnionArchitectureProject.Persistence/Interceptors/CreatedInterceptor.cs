using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OnionArchitectureProject.Domain.Base;
using System.Security.Claims;

namespace OnionArchitectureProject.Persistence.Interceptors;

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

        var currentUserId = httpContextAccessor?.HttpContext?.User
            .FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "f37c60d7-47b8-4375-a226-77eaa6fe8885";

        foreach (var entry in entries)
        {
            entry.Entity.CreatedBy = currentUserId;
            entry.Entity.CreatedUtcDate = DateTime.UtcNow;
        }
    }
}