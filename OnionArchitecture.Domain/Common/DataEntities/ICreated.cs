namespace OnionArchitecture.Domain.Common.DataEntities;
public interface ICreated
{
    DateTime CreatedDateUTC { get; set; }
    string CreatedBy { get; set; }
}