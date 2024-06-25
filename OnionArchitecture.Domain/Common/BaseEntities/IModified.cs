namespace OnionArchitecture.Domain.Common.BaseEntities;
public interface IModified
{
    DateTime? ModifiedDateUTC { get; set; }
    string? ModifiedBy { get; set; }
}