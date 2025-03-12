using AutoMapper;
using OnionArchitectureProject.Application.Admin.RoleService.Models;
using OnionArchitectureProject.Application.Admin.RoleService.Models.UpsertRoleDto;
using OnionArchitectureProject.Authentication.Models;

namespace OnionArchitectureProject.Authentication.Services.RoleService;
public class ApplicationRoleProfile : Profile
{
    public ApplicationRoleProfile()
    {
        CreateMap<ApplicationRole, RoleDto>()
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

        CreateMap<UpsertRoleDto, ApplicationRole>();
        CreateMap<RoleDto, UpsertRoleDto>();
    }
}