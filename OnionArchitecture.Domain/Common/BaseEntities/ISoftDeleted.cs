namespace OnionArchitecture.Domain.Common.BaseEntities;
public interface ISoftDeleted
{
    bool IsDeleted { get; set; }
    //DateTime? DeletedDateUTC { get; set; }
}