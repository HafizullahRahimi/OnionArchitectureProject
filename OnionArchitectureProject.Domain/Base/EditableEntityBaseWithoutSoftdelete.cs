namespace OnionArchitectureProject.Domain.Base;
public abstract class EditableEntityBaseWithoutSoftdelete<TId> : EntityBase<TId>, IModified where TId : notnull
{
    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedBy { get; set; }
}