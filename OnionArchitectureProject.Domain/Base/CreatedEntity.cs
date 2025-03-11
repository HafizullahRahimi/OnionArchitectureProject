namespace OnionArchitectureProject.Domain.Base;

public class CreatedEntity<TId> : BaseEntity<TId>, ICreatedEntity
    where TId : notnull
{
    public DateTime CreatedUtcDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
}