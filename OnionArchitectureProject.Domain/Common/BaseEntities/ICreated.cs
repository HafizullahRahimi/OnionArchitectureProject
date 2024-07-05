namespace OnionArchitectureProject.Domain.Common.BaseEntities;
public interface ICreated
{
    DateTime CreatedAt { get; set; }
    string CreatedBy { get; set; }
}