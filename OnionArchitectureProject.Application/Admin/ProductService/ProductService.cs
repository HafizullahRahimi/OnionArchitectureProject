using System.Threading;
using AutoMapper;
using OnionArchitectureProject.Application.Admin.ProductService.Models;
using OnionArchitectureProject.Application.Common.Models;
using OnionArchitectureProject.Domain.Authentication.Users;
using OnionArchitectureProject.Domain.Categories;
using OnionArchitectureProject.Domain.Products;

namespace OnionArchitectureProject.Application.Admin.ProductService;

public class ProductService : IProductService
{
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;
    private readonly IUserRepository userRepository;
    private readonly ICategoryRepository categoryRepository;

    public ProductService(
        IProductRepository productRepository,
        IMapper mapper,
        IUserRepository userRepository,
        ICategoryRepository categoryRepository)
    {
        this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        this.categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
    }

    public async Task<List<ProductDto>> GetProductsAsync(CancellationToken cancellationToken)
    {
        var products = await productRepository.WithCategory().ToListAsync(cancellationToken);

        var userIds = products
            .SelectMany(p => new[] { p.CreatedBy, p.ModifiedBy })
            .Where(id => !string.IsNullOrEmpty(id))
            .Distinct()
            .ToList();

        var users = await GetUsersByIdsAsync(userIds, cancellationToken);
        var userDictionary = users.ToDictionary(u => u.Id);

        return products.Select(product => MapToProductDto(product, userDictionary)).ToList();
    }

    public async Task<List<Category>?> GetAvailableCategoriesAsync(CancellationToken cancellationToken) =>
        await categoryRepository.GetAllAsync(cancellationToken);

    public async Task<OperationResult> CreateAsync(UpsertProductDto upsertProductDto, CancellationToken cancellationToken)
    {
        try
        {
            var product = mapper.Map<Product>(upsertProductDto);
            product = await productRepository.CreateAsync(product, cancellationToken);
            return new OperationResult(true, null);
        }
        catch (Exception)
        {
            return new OperationResult(false, $"Error creating product");
        }
    }

    public UpsertProductDto MapToUpsertProductDto(ProductDto productDto)
    {
        return mapper.Map<UpsertProductDto>(productDto);
    }

    public async Task<OperationResult> UpdateAsync(UpsertProductDto upsertProductDto, CancellationToken cancellationToken)
    {
        try
        {
            var existingProduct = await GetProductByIdAsync(upsertProductDto.Id ?? Guid.Empty, cancellationToken);
            if (existingProduct == null)
            {
                return new OperationResult(false, $"Product with ID '{upsertProductDto.Id}' not found.");
            }

            mapper.Map(upsertProductDto, existingProduct);
            await productRepository.UpdateAsync(existingProduct, cancellationToken);
            return new OperationResult(true, null);
        }
        catch (Exception)
        {
            return new OperationResult(false, $"Error updating product");
        }
    }

    public async Task<OperationResult> DeleteAsync(Guid productId, CancellationToken cancellationToken)
    {
        try
        {
            var existingProduct = await GetProductByIdAsync(productId, cancellationToken);
            if (existingProduct == null)
            {
                return new OperationResult(false, $"Product with ID '{productId}' not found.");
            }

            await productRepository.DeleteAsync(existingProduct, cancellationToken);
            return new OperationResult(true, null);
        }
        catch (Exception)
        {
            return new OperationResult(false, $"Error deleting product");
        }
    }

    private ProductDto MapToProductDto(Product product, Dictionary<string, User> userDictionary)
    {
        var productDto = mapper.Map<ProductDto>(product);

        if (!userDictionary.TryGetValue(product.CreatedBy, out var createdByUser))
        {
            throw new InvalidOperationException($"User with ID {product.CreatedBy} not found for product {product.Id}");
        }
        productDto.CreatedBy = createdByUser;

        productDto.ModifiedBy = userDictionary.GetValueOrDefault(product.ModifiedBy);

        return productDto;
    }

    private async Task<List<User>> GetUsersByIdsAsync(List<string> userIds, CancellationToken cancellationToken)
    {
        if (!userIds.Any())
            return new List<User>();

        var users = new List<User>();
        foreach (var userId in userIds)
        {
            var user = await userRepository.GetByIdAsync(userId);
            if (user != null)
                users.Add(user);
        }
        return users;
    }

    private async Task<Product?> GetProductByIdAsync(Guid productId, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(productId, cancellationToken);
        return product;
    }
}