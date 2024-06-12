namespace OnionArchitecture.Domain.Common.Interfaces;
public interface ISoftDeleted
{
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}