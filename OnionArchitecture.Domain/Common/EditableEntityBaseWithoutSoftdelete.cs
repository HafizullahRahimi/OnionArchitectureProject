using OnionArchitecture.Domain.Common.Interfaces;

namespace OnionArchitecture.Domain.Common;
public abstract class EditableEntityBaseWithoutSoftdelete<TId> : EntityBase<TId>, IModified where TId : notnull
{
    public DateTime? ModifiedDateUTC { get; set; }
    public string? ModifiedBy { get; set; }
}