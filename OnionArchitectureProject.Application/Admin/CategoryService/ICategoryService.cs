using OnionArchitectureProject.Application.Admin.CategoryService.Models;
using OnionArchitectureProject.Application.Admin.CategoryService.Models.UpsertCategoryDto;
using OnionArchitectureProject.Application.Common;

namespace OnionArchitectureProject.Application.Admin.CategoryService;
public interface ICategoryService
{
    Task<List<CategoryDto>?> GetCategoriesAsync(CancellationToken cancellationToken);
    Task<OperationResult> CreateAsync(UpsertCategoryDto upsertCategoryDto, CancellationToken cancellationToken);
    Task<OperationResult> UpdateAsync(UpsertCategoryDto upsertCategoryDto, CancellationToken cancellationToken);
    Task<OperationResult> DeleteAsync(Guid categoryId, CancellationToken cancellationToken);
    UpsertCategoryDto MapToUpsertCategoryDto(CategoryDto categoryDto);
}