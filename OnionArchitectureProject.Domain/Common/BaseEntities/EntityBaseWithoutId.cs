namespace OnionArchitectureProject.Domain.Common.BaseEntities;
public class EntityBaseWithoutId : ICreated
{
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
}