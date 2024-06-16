namespace OnionArchitecture.Domain.Common.Interfaces;
public interface ICreated
{
    DateTime CreatedDateUTC { get; set; }
    string CreatedBy { get; set; }
}