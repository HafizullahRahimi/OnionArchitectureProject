namespace OnionArchitecture.Domain.Common.BaseEntities;
public abstract class EditableEntityBaseWithoutSoftdelete<TId> : EntityBase<TId>, IModified where TId : notnull
{
    public DateTime? ModifiedDateUTC { get; set; }
    public string? ModifiedBy { get; set; }
}