using System.ComponentModel.DataAnnotations;

namespace OnionArchitectureProject.Domain.Common.BaseEntities;
public class EntityBase<TId> : EntityBaseWithoutId
{
    [Key] public TId Id { get; set; } = default!;
}