namespace OnionArchitectureProject.Domain.Base;

public interface ICreatedEntity
{
    DateTime CreatedUtcDate { get; set; }
    string CreatedBy { get; set; }
}