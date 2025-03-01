using AutoMapper;
using OnionArchitectureProject.Authentication.Models;
using OnionArchitectureProject.Domain.Authentication.Roles;

namespace OnionArchitectureProject.Authentication.Repositories.RoleRepository.Profiles;
public class ApplicationRoleProfile : Profile
{
    public ApplicationRoleProfile()
    {
        CreateMap<ApplicationRole, Role>().ReverseMap();
    }
}