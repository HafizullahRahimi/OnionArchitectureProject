using AutoMapper;
using OnionArchitectureProject.Application.Admin.ProductService.Models;
using OnionArchitectureProject.Domain.Authentication.Users;
using OnionArchitectureProject.Domain.Products;

namespace OnionArchitectureProject.Application.Admin.ProductService;
public class ProductService : IProductService
{
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;
    private readonly IUserRepository userRepository;

    public ProductService(
        IProductRepository productRepository,
        IMapper mapper,
        IUserRepository userRepository)
    {
        this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
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
}