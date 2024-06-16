namespace OnionArchitecture.Domain.Common.Interfaces;
public interface IModified
{
    DateTime? ModifiedDateUTC { get; set; }
    string? ModifiedBy { get; set; }
}