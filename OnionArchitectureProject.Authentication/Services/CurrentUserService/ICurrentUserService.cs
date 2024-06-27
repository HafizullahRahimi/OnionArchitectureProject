namespace OnionArchitectureProject.Authentication.Services.CurrentUserService;
public interface ICurrentUserService
{
    string GetId();
    string GetName();
    string GetEmail();
}