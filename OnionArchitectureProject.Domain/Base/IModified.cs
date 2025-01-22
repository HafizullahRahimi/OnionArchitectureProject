namespace OnionArchitectureProject.Domain.Base;
public interface IModified
{
    DateTime ModifiedDateUtc { get; set; }
    string ModifiedBy { get; set; }
}