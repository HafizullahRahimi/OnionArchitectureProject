namespace OnionArchitectureProject.Domain.Base;
public abstract class EditableAuthEntityBase : IEditableAuthEntityBase
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime ModifiedDateUtc { get; set; }
    public string ModifiedBy { get; set; } = string.Empty;
    public string ConcurrencyStamp { get; set; } = string.Empty;
    public DateTime CreatedDateUtc { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
}
