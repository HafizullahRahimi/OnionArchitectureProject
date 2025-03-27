using AutoMapper;
using OnionArchitectureProject.Application.Admin.UserService.Models;
using OnionArchitectureProject.Authentication.Models;

namespace OnionArchitectureProject.Authentication.Services.Admin.UserService.Models;
public class ApplicationUserProfile : Profile
{
    public ApplicationUserProfile()
    {
        CreateMap<ApplicationUser, UserDto>()
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

        CreateMap<CreateUserDto, ApplicationUser>();
    }
}