using OnionArchitecture.Domain.Common.Interfaces;

namespace OnionArchitecture.Domain.Common;
public class SoftdeleteableEntityBase<TId> : EntityBase<TId>, ISoftDeleted where TId : notnull
{
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedDateUTC { get; set; }
    public string? DeletedBy { get; set; } = string.Empty;
}