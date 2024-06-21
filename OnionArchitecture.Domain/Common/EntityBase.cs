using System.ComponentModel.DataAnnotations;

namespace OnionArchitecture.Domain.Common;
public abstract class EntityBase<TId> : EntityBaseWithoutId
{
    [Key] public TId Id { get; set; } = default!;
}