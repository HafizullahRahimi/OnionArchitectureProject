namespace OnionArchitectureProject.Domain.Base;
public interface ICreated
{
    DateTime CreatedAt { get; set; }
    string CreatedBy { get; set; }
}