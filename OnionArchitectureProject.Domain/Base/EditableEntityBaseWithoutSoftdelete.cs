namespace OnionArchitectureProject.Domain.Base;
public abstract class EditableEntityBaseWithoutSoftdelete<TId> : EntityBase<TId>, IModified where TId : notnull
{
    public DateTime ModifiedDateUtc { get; set; }
    public string ModifiedBy { get; set; } = string.Empty;
}