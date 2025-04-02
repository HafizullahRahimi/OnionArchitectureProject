using AutoMapper;
using OnionArchitectureProject.Domain.Authentication.Roles;

namespace OnionArchitectureProject.Application.Admin.RoleService.Models;
public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<Role, RoleDto>()
            .ForMember(dest => dest.CreatedByUserName, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedLocalTime, opt =>
            {
                opt.PreCondition(src => src.CreatedUtcDate != DateTime.MinValue);
                opt.MapFrom(src => src.CreatedUtcDate.ToLocalTime());
            })
            .ForMember(dest => dest.ModifiedByUserName, opt => opt.MapFrom(src => src.ModifiedBy))
            .ForMember(dest => dest.ModifiedLocalTime, opt =>
            {
                opt.PreCondition(src => src.ModifiedUtcDate != DateTime.MinValue);
                opt.MapFrom(src => src.ModifiedUtcDate.ToLocalTime());
            });

        CreateMap<UpsertRoleDto, Role>();
        CreateMap<RoleDto, UpsertRoleDto>();
    }
}