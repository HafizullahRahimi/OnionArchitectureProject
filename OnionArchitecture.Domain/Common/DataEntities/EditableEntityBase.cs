namespace OnionArchitecture.Domain.Common.DataEntities;
public abstract class EditableEntityBase<TId> : EditableEntityBaseWithoutSoftdelete<TId>, ISoftDeleted where TId : notnull
{
    public bool IsDeleted { get; set; } = false;
}