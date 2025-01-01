namespace OnionArchitectureProject.Domain.Base;
public interface ISoftDeleted
{
    bool IsDeleted { get; set; }
    //DateTime? DeletedAt { get; set; }
}