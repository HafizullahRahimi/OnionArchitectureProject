using OnionArchitectureProject.Application.Admin.ProductService.Models;
using OnionArchitectureProject.Application.Admin.ProductService.Models.UpsertProductDto;

namespace OnionArchitectureProject.Application.Admin.ProductService;
public interface IProductService
{
    Task<List<ProductDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<ProductDto> GetByIdAsync(Guid productId, CancellationToken cancellationToken);
    Task<Guid> CreateAsync(UpsertProductDto productDto, CancellationToken cancellationToken);
    Task UpdateAsync(Guid productId, UpsertProductDto productDto, CancellationToken cancellationToken);
    Task<bool> ExistAsync(Guid productId, CancellationToken cancellationToken);
    Task DeleteAsync(Guid productId, CancellationToken cancellationToken);
}