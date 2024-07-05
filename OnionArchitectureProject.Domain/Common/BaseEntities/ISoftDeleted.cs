namespace OnionArchitectureProject.Domain.Common.BaseEntities;
public interface ISoftDeleted
{
    bool IsDeleted { get; set; }
    //DateTime? DeletedAt { get; set; }
}