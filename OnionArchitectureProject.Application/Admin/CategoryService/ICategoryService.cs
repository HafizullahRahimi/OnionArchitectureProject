using OnionArchitectureProject.Application.Admin.CategoryService.Models;
using OnionArchitectureProject.Application.Admin.CategoryService.Models.UpsertCategoryDto;

namespace OnionArchitectureProject.Application.Admin.CategoryService;
public interface ICategoryService
{
    Task<List<CategoryDto>?> GetCategoriesAsync(CancellationToken cancellationToken);
    Task<bool> CreateAsync(UpsertCategoryDto upsertCategoryDto, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(UpsertCategoryDto upsertCategoryDto, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid categoryId, CancellationToken cancellationToken);
    UpsertCategoryDto MapToUpsertCategoryDto(CategoryDto categoryDto);
}