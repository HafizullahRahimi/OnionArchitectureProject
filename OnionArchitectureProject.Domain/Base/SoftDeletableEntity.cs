namespace OnionArchitectureProject.Domain.Base;

public abstract class SoftDeletableEntity<TId> : ModifiedEntity<TId>, ISoftDeletableEntity
    where TId : notnull
{
    public bool IsDeleted { get; set; }
    //public DateTime? DeletedUtcDate { get; set; }
    //public string? DeletedBy { get; set; }
}