using System.ComponentModel.DataAnnotations;

namespace OnionArchitecture.Domain.Common;
public abstract class EntityBase<TId>
{
    [Key] public TId Id { get; set; } = default!;
}