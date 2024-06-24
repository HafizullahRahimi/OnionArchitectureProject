using AutoMapper;
using OnionArchitecture.Domain.Products;
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

    public async Task<Guid> CreateAsync(UpsertProductDto productDto, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(productDto);
        product = await _productRepository.CreateAsync(product, cancellationToken);
        return product.Id;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _productRepository.DeleteAsync(id, cancellationToken);
    }

    public Task<bool> ExistAsync(Guid id, CancellationToken cancellationToken) =>
        _productRepository.ExistAsync(id, cancellationToken);

    public async Task<List<ProductDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllWithCategoryAsync(cancellationToken);
        var ProductDtos = _mapper.Map<List<ProductDto>>(products);
        return ProductDtos;
    }

    public async Task<ProductDto> GetByIdAsync(Guid productId, CancellationToken cancellationToken)
    {

        var product = await _productRepository.GetByIdWithCategoryAsync(productId, cancellationToken);
        var ProductDto = _mapper.Map<ProductDto>(product);
        return ProductDto;
    }

    public async Task UpdateAsync(Guid productId, UpsertProductDto productDto, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(productId, cancellationToken);

        if (product != null)
        {
            _mapper.Map(productDto, product);
            await _productRepository.UpdateAsync(product, cancellationToken);
        }

    }
}