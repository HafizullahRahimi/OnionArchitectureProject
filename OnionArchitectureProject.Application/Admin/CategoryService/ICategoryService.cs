using OnionArchitectureProject.Application.Admin.CategoryService.Models;
using OnionArchitectureProject.Application.Admin.CategoryService.Models.UpsertCategoryDto;

namespace OnionArchitectureProject.Application.Admin.CategoryService;
public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetCategoriesAsync(CancellationToken cancellationToken);
    Task<UpsertCategoryDto?> GetByIdAsync(Guid categoryId, CancellationToken cancellationToken);
    Task<Guid> CreateAsync(UpsertCategoryDto categoryDto, CancellationToken cancellationToken);
    Task<bool> ExistAsync(Guid categoryId, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(UpsertCategoryDto category, CancellationToken cancellationToken);
    Task DeleteAsync(Guid categoryId, CancellationToken cancellationToken);
    UpsertCategoryDto MapToUpsertCategoryDto(CategoryDto categoryDto);
}