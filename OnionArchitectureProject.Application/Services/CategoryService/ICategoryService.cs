using OnionArchitectureProject.Application.Services.CategoryService.Models.CategoryDto;
using OnionArchitectureProject.Domain.Categories;

namespace OnionArchitectureProject.Application.Services.CategoryService;
public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken);
    Task<CategoryDto?> GetByIdAsync(Guid categoryId, CancellationToken cancellationToken);
    Task<Guid> CreateAsync(CategoryDto categoryDto, CancellationToken cancellationToken);
    Task<bool> ExistAsync(Guid categoryId, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Guid categoryId, CategoryDto categoryDto, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Category category, CancellationToken cancellationToken);
    Task DeleteAsync(Guid categoryId, CancellationToken cancellationToken);
}