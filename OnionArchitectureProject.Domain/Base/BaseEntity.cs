using System.ComponentModel.DataAnnotations;

namespace OnionArchitectureProject.Domain.Base;

public class BaseEntity<TId> : IEntity<TId>
    where TId : notnull
{
    [Key]
    public TId Id { get; set; } = default!;
}