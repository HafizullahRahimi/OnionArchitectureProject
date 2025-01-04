using AutoMapper;
using OnionArchitectureProject.Domain.Categories;
using OnionArchitectureProject.Application.Admin.CategoryService.Models.UpsertCategoryDto;
using OnionArchitectureProject.Application.Admin.CategoryService.Models;

namespace OnionArchitectureProject.Application.Admin.CategoryService.Profiles;
public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, UpsertCategoryDto>().ReverseMap();
        CreateMap<Category, Category>();
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<UpsertCategoryDto, CategoryDto>().ReverseMap();
    }
}