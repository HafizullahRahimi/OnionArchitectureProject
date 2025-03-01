using AutoMapper;
using OnionArchitectureProject.Application.Admin.RoleService.Models;
using OnionArchitectureProject.Application.Admin.RoleService.Models.UpsertRoleDto;
using OnionArchitectureProject.Domain.Authentication.Roles;

namespace OnionArchitectureProject.Application.Admin.RoleService.Profiles;
public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<Role, RoleDto>()
            .ForMember(dest => dest.CreatedByUserName, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedLocalTime, opt =>
            {
                opt.PreCondition(src => src.CreatedDateUtc != DateTime.MinValue);
                opt.MapFrom(src => src.CreatedDateUtc.ToLocalTime());
            })
            .ForMember(dest => dest.ModifiedByUserName, opt => opt.MapFrom(src => src.ModifiedBy))
            .ForMember(dest => dest.ModifiedLocalTime, opt =>
            {
                opt.PreCondition(src => src.ModifiedDateUtc != DateTime.MinValue);
                opt.MapFrom(src => src.ModifiedDateUtc.ToLocalTime());
            });

        CreateMap<UpsertRoleDto, Role>();
        CreateMap<RoleDto, UpsertRoleDto>();
    }
}