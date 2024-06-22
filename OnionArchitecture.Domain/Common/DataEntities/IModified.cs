namespace OnionArchitecture.Domain.Common.DataEntities;
public interface IModified
{
    DateTime? ModifiedDateUTC { get; set; }
    string? ModifiedBy { get; set; }
}