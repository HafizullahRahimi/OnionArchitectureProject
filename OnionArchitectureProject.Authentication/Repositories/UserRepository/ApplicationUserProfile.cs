using AutoMapper;
using OnionArchitectureProject.Application.Authentication.Roles;
using OnionArchitectureProject.Authentication.Models;
using OnionArchitectureProject.Domain.Authentication.Users;

namespace OnionArchitectureProject.Authentication.Repositories.UserRepository;
public class ApplicationUserProfile : Profile
{
    public ApplicationUserProfile()
    {
        CreateMap<ApplicationUser, User>().ReverseMap();
    }
}