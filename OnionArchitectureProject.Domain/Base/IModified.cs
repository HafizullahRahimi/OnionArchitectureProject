namespace OnionArchitectureProject.Domain.Base;
public interface IModified
{
    DateTime? ModifiedAt { get; set; }
    string? ModifiedBy { get; set; }
}