using OnionArchitectureProject.Domain.Base.Repositories;

namespace OnionArchitectureProject.Domain.Products;

public interface IProductRepository : IRepository<Product>, IProductRepositoryWithFilter { }