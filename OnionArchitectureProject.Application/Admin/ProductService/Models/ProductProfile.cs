using AutoMapper;
using OnionArchitectureProject.Domain.Products;

namespace OnionArchitectureProject.Application.Admin.ProductService.Models;
public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedLocalTime, opt =>
            {
                opt.PreCondition(src => src.CreatedUtcDate != DateTime.MinValue);
                opt.MapFrom(src => src.CreatedUtcDate.ToLocalTime());
            })
            .ForMember(dest => dest.ModifiedLocalTime, opt =>
            {
                opt.PreCondition(src => src.ModifiedUtcDate != DateTime.MinValue);
                opt.MapFrom(src => src.ModifiedUtcDate.ToLocalTime());
            });
    }
}