using System.ComponentModel.DataAnnotations;

namespace OnionArchitecture.Domain.Common;
public class EntityBase<TId>
{
    [Key] public TId Id { get; set; } = default!;
}