using AutoMapper;
using OnionArchitectureProject.Domain.Products;
using OnionArchitectureProject.Application.Admin.ProductService.Models;
using OnionArchitectureProject.Application.Admin.ProductService.Models.UpsertProductDto;

namespace OnionArchitectureProject.Application.Admin.ProductService.Profiles;
public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Product, UpsertProductDto>().ReverseMap();
    }
}