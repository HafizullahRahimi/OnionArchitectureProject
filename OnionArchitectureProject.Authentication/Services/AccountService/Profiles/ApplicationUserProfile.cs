using AutoMapper;
using OnionArchitectureProject.Authentication.Models;
using OnionArchitectureProject.Authentication.Services.AccountService.Models;

namespace OnionArchitectureProject.Authentication.Services.AccountService.Profiles;
public class ApplicationUserProfile: Profile
{
    public ApplicationUserProfile()
    {
        CreateMap<ApplicationUser, RegisterDto>().ReverseMap();
    }
}