using OnionArchitecture.Domain.Common.Interfaces;

namespace OnionArchitecture.Domain.Common;
public abstract class EntityBaseWithoutId : ICreated
{
    public DateTime CreatedDateUTC { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
}