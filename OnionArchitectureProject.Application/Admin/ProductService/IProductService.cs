using OnionArchitectureProject.Application.Admin.ProductService.Models;
using OnionArchitectureProject.Application.Common.Models;
using OnionArchitectureProject.Domain.Categories;

namespace OnionArchitectureProject.Application.Admin.ProductService;

public interface IProductService
{
    Task<List<ProductDto>> GetProductsAsync(CancellationToken cancellationToken);
    Task<List<Category>?> GetAvailableCategoriesAsync(CancellationToken cancellationToken);
    Task<OperationResult> CreateAsync(UpsertProductDto upsertProductDto, CancellationToken cancellationToken);
    UpsertProductDto MapToUpsertProductDto(ProductDto productDto);
    Task<OperationResult> UpdateAsync(UpsertProductDto upsertProductDto, CancellationToken cancellationToken);
}