namespace OnionArchitectureProject.Domain.Base;

public class ModifiedEntity<TId> : CreatedEntity<TId>, IModifiedEntity
    where TId : notnull
{
    public DateTime ModifiedUtcDate { get; set; }
    public string ModifiedBy { get; set; } = string.Empty;
}
