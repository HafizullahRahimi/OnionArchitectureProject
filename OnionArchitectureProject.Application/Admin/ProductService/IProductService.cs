using OnionArchitectureProject.Application.Admin.ProductService.Models;

namespace OnionArchitectureProject.Application.Admin.ProductService;
public interface IProductService
{
    Task<List<ProductDto>> GetProductsAsync(CancellationToken cancellationToken);
}