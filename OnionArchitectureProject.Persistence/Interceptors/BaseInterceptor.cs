using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Security.Claims;

namespace OnionArchitectureProject.Persistence.Interceptors;

public abstract class BaseInterceptor : SaveChangesInterceptor
{
    protected readonly IHttpContextAccessor? httpContextAccessor;

    protected BaseInterceptor(IHttpContextAccessor? httpContextAccessor = null)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public string CurrentUserId => httpContextAccessor?.HttpContext?.User
            .FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Unknown";

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        ProcessEntities(eventData);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    protected abstract void ProcessEntities(DbContextEventData eventData);
}