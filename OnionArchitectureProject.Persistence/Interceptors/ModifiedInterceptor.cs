using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OnionArchitectureProject.Domain.Common.BaseEntities;
using Microsoft.AspNetCore.Http;
using OnionArchitectureProject.Persistence.Extensions;

namespace OnionArchitectureProject.Persistence.Extensions;
public class ModifiedInterceptor : SaveChangesInterceptor
{

    private readonly IHttpContextAccessor _httpContextAccessor;

    public ModifiedInterceptor(IHttpContextAccessor httpContextAccessor)
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

        IEnumerable<EntityEntry<IModified>> entries =
              eventData
              .Context
              .ChangeTracker.Entries<IModified>()
              .Where(_ => _.State == Microsoft.EntityFrameworkCore.EntityState.Modified);

        var JwtToken = _httpContextAccessor.GetJwtToken();
        var currentUserName = string.Empty;
        if (JwtToken != null)
            currentUserName = JwtToken.GetCurrentUserName();

        foreach (EntityEntry<IModified> softDeletable in entries)
        {
            softDeletable.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            softDeletable.Entity.ModifiedBy = currentUserName;
            softDeletable.Entity.ModifiedDateUTC = DateTime.Now;
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}