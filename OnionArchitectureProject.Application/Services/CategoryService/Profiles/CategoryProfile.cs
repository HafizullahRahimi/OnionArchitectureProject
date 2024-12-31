using AutoMapper;
using OnionArchitectureProject.Domain.Categories;
using OnionArchitectureProject.Application.Services.CategoryService.Models.UpsertCategoryDto;
using OnionArchitectureProject.Application.Services.CategoryService.Models;

namespace OnionArchitectureProject.Application.Profiles;
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