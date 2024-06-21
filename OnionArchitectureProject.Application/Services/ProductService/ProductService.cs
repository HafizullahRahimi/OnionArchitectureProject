using AutoMapper;
using OnionArchitecture.Domain.Entities;
using OnionArchitectureProject.Application.Contracts.Persistence.IRepositories;
using OnionArchitectureProject.Application.Profiles;
using OnionArchitectureProject.Application.Services.ProductService.Models;

namespace OnionArchitectureProject.Application.Services.ProductService;
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        var mapperConfig = new MapperConfiguration(m =>
        {
            m.AddProfile<ProductProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    public async Task<List<ProductDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync(cancellationToken);
        var ProductDtos = _mapper.Map<List<ProductDto>>(products);
        return ProductDtos;
    }

    public async Task<ProductDto> GetByIdAsync(Guid productId, CancellationToken cancellationToken)
    {

        var product = await _productRepository.GetByIdAsync(productId, cancellationToken);
        var ProductDto = _mapper.Map<ProductDto>(product);
        return ProductDto;
    }
}