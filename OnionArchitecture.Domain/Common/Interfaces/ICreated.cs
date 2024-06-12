namespace OnionArchitecture.Domain.Common.Interfaces;
public interface ICreated
{
    DateTime CreatedAt { get; set; }
    string CreatedBy { get; set; }
}