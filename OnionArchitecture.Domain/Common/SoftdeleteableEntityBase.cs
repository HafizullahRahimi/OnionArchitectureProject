using OnionArchitecture.Domain.Common.Interfaces;

namespace OnionArchitecture.Domain.Common;
public class SoftdeleteableEntityBase<TId> : EntityBase<TId>, ISoftDeleted
{
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; } = string.Empty;
}