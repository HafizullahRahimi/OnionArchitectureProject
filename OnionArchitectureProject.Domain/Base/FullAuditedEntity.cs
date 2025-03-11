namespace OnionArchitectureProject.Domain.Base;

public abstract class FullAuditedEntity<TId> : SoftDeletableEntity<TId>
    where TId : notnull
{
}