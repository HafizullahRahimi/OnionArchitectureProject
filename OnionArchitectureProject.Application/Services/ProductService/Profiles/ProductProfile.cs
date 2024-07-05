using AutoMapper;
using OnionArchitectureProject.Domain.Products;
using OnionArchitectureProject.Application.Services.ProductService.Models;

namespace OnionArchitectureProject.Application.Profiles;
public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Product, UpsertProductDto>().ReverseMap();
    }
}