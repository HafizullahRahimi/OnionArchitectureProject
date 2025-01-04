namespace OnionArchitectureProject.Domain.Base;
public class EntityBaseWithoutId : ICreated
{
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string CreatedBy { get; set; } = string.Empty;
}