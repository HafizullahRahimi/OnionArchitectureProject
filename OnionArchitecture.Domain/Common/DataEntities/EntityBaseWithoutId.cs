namespace OnionArchitecture.Domain.Common.DataEntities;
public class EntityBaseWithoutId : ICreated
{
    public DateTime CreatedDateUTC { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
}