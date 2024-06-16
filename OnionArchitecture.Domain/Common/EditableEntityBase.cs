using OnionArchitecture.Domain.Common.Interfaces;

namespace OnionArchitecture.Domain.Common;
public class EditableEntityBase<TId> : EntityBase<TId>, ICreated, IModified where TId : notnull
{
    public DateTime CreatedDateUTC { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime? ModifiedDateUTC { get; set; }
    public string? ModifiedBy { get ; set; }
}