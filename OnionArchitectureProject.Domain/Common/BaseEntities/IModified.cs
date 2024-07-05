namespace OnionArchitectureProject.Domain.Common.BaseEntities;
public interface IModified
{
    DateTime? ModifiedAt { get; set; }
    string? ModifiedBy { get; set; }
}