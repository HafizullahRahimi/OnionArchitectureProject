using AutoMapper;
using OnionArchitectureProject.Domain.Categories;
using OnionArchitectureProject.Application.Admin.CategoryService.Models.UpsertCategoryDto;
using OnionArchitectureProject.Application.Admin.CategoryService.Models;

namespace OnionArchitectureProject.Application.Admin.CategoryService.Profiles;
public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>()
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

        CreateMap<UpsertCategoryDto, Category>().ReverseMap();
        CreateMap<CategoryDto, UpsertCategoryDto>();
    }
}