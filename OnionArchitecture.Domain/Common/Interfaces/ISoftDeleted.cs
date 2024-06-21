namespace OnionArchitecture.Domain.Common.Interfaces;
public interface ISoftDeleted
{
    bool IsDeleted { get; set; }
    //DateTime? DeletedDateUTC { get; set; }
}