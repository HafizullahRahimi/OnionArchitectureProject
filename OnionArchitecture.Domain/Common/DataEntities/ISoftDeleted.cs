namespace OnionArchitecture.Domain.Common.DataEntities;
public interface ISoftDeleted
{
    bool IsDeleted { get; set; }
    //DateTime? DeletedDateUTC { get; set; }
}