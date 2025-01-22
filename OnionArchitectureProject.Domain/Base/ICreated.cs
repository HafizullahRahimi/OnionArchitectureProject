namespace OnionArchitectureProject.Domain.Base;
public interface ICreated
{
    DateTime CreatedDateUtc { get; set; }
    string CreatedBy { get; set; }
}