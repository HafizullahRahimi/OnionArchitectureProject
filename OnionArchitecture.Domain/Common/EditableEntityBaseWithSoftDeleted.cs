using OnionArchitecture.Domain.Common.Interfaces;

namespace OnionArchitecture.Domain.Common;
public class EditableEntityBaseWithSoftDeleted<TId> : EditableEntityBase<TId>, ISoftDeleted where TId : notnull
{
    public bool IsDeleted { get; set; }
    public DateTime? DeletedDateUTC { get; set; }
}