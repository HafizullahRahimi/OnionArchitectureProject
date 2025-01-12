namespace OnionArchitectureProject.Domain.Base;
public interface IEntityBase<TId>
{
    TId Id { get; set; }
}