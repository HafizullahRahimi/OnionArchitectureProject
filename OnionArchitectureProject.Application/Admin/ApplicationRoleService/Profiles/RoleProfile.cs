using AutoMapper;
using OnionArchitectureProject.Application.Admin.ApplicationRoleService.Models;
using OnionArchitectureProject.Application.Admin.ApplicationRoleService.Models.UpsertRoleDto;
using OnionArchitectureProject.Domain.Authentication;

namespace OnionArchitectureProject.Application.Admin.ApplicationRoleService.Profiles;
public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<UpsertRoleDto, ApplicationRole>();
        CreateMap<ApplicationRole, RoleDto>();
        CreateMap<RoleDto, UpsertRoleDto>();
    }
}