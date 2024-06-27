using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OnionArchitectureProject.Domain.Common.BaseEntities;

namespace OnionArchitectureProject.Persistence.Extensions;
public class CreatedInterceptor : SaveChangesInterceptor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreatedInterceptor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
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

        var JwtToken = _httpContextAccessor.GetJwtToken();
        var currentUserName = string.Empty;
        if (JwtToken != null)
            currentUserName = JwtToken.GetCurrentUserName();


        foreach (EntityEntry<ICreated> softDeletable in entries)
        {
            softDeletable.State = Microsoft.EntityFrameworkCore.EntityState.Added;
            softDeletable.Entity.CreatedBy = currentUserName;
            softDeletable.Entity.CreatedDateUTC = DateTime.Now;
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}