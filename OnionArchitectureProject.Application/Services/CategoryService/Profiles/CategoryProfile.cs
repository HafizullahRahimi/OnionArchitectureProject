using AutoMapper;
using OnionArchitectureProject.Domain.Categories;
using OnionArchitectureProject.Application.Services.CategoryService.Models;

namespace OnionArchitectureProject.Application.Profiles;
public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
    }
}