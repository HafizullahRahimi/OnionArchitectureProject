using OnionArchitectureProject.Application.Services.ProductService.Models;

namespace OnionArchitectureProject.Application.Services.ProductService;
public interface IProductService
{
    Task<List<ProductDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<ProductDto> GetByIdAsync(Guid productId, CancellationToken cancellationToken);
    Task<ProductDto> CreateAsync(CreateProductDto productDto, CancellationToken cancellationToken);
    Task<ProductDto> UpdateAsync(CreateProductDto productDto, CancellationToken cancellationToken);
    Task<bool> ExistAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}