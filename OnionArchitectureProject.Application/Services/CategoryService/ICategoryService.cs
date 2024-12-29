using OnionArchitectureProject.Application.Services.CategoryService.Models;
using OnionArchitectureProject.Application.Services.CategoryService.Models.UpsertCategoryDto;
using OnionArchitectureProject.Domain.Categories;

namespace OnionArchitectureProject.Application.Services.CategoryService;
public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetCategoriesAsync(CancellationToken cancellationToken);
    Task<UpsertCategoryDto?> GetByIdAsync(Guid categoryId, CancellationToken cancellationToken);
    Task<Guid> CreateAsync(UpsertCategoryDto categoryDto, CancellationToken cancellationToken);
    Task<bool> ExistAsync(Guid categoryId, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(UpsertCategoryDto category, CancellationToken cancellationToken);
    Task DeleteAsync(Guid categoryId, CancellationToken cancellationToken);
}