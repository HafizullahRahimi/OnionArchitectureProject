namespace OnionArchitecture.Domain.Common.Interfaces;
public interface IModified
{
    DateTime? ModifiedAt { get; set; }
    string? ModifiedBy { get; set; }
}