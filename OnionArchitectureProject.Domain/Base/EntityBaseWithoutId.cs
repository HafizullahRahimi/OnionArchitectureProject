namespace OnionArchitectureProject.Domain.Base;
public class EntityBaseWithoutId : ICreated
{
    public DateTime CreatedDateUtc { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
}