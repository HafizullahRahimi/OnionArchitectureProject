using OnionArchitecture.Domain.Entities;
using OnionArchitectureProject.Application.Contracts.Persistence.IRepositories.Common;

namespace OnionArchitectureProject.Application.Contracts.Persistence.IRepositories;
public interface IProductRepository : IRepository<Product>
{
}