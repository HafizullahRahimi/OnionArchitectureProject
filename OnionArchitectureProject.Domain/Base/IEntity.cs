namespace OnionArchitectureProject.Domain.Base;

public interface IEntity<TId> where TId : notnull
{
    TId Id { get; set; }
}