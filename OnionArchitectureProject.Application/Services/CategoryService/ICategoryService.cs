using OnionArchitectureProject.Application.Services.CategoryService.Models;

namespace OnionArchitectureProject.Application.Services.CategoryService;
public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<CategoryDto?> GetByIdAsync(Guid categoryId, CancellationToken cancellationToken);
    Task<Guid> CreateAsync(string categoryName, string createdBy, CancellationToken cancellationToken);
    Task<bool> ExistAsync(Guid categoryId, CancellationToken cancellationToken);
    Task UpdateAsync(Guid categoryId, string categoryName, string modifiedBy, CancellationToken cancellationToken);
    Task DeleteAsync(Guid categoryId, CancellationToken cancellationToken);
}