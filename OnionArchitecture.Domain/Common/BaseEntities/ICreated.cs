namespace OnionArchitecture.Domain.Common.BaseEntities;
public interface ICreated
{
    DateTime CreatedDateUTC { get; set; }
    string CreatedBy { get; set; }
}