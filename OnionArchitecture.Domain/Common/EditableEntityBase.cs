using OnionArchitecture.Domain.Common.Interfaces;

namespace OnionArchitecture.Domain.Common;
public abstract class EditableEntityBase<TId> : EditableEntityBaseWithoutSoftdelete<TId>, ISoftDeleted where TId : notnull
{
    public bool IsDeleted { get; set; } = false;
}