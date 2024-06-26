namespace OnionArchitectureProject.Domain.Common.BaseEntities;
public abstract class EditableEntityBase<TId> : EditableEntityBaseWithoutSoftdelete<TId>, ISoftDeleted where TId : notnull
{
    public bool IsDeleted { get; set; } = false;
}