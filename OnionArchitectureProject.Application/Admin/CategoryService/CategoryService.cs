using AutoMapper;
using OnionArchitectureProject.Application.Admin.CategoryService.Models;
using OnionArchitectureProject.Application.Admin.CategoryService.Models.UpsertCategoryDto;
using OnionArchitectureProject.Application.Common.Models;
using OnionArchitectureProject.Domain.Authentication;
using OnionArchitectureProject.Domain.Categories;

namespace OnionArchitectureProject.Application.Admin.CategoryService;
public class CategoryService(IAuthenticationRepository authenticationRepository, ICategoryRepository categoryRepository, IMapper mapper) : ICategoryService
{
    private readonly IAuthenticationRepository authenticationRepository = authenticationRepository;
    private readonly ICategoryRepository categoryRepository = categoryRepository;
    private readonly IMapper mapper = mapper;

    public async Task<List<CategoryDto>?> GetCategoriesAsync(CancellationToken cancellationToken)
    {
        try
        {
            // Start Example Category Repository Filter ----------------------------------------

            ////  1- With Created At UtcTime
            //var timeOnly = new TimeOnly(13, 38, 24);
            //var categories = await categoryRepository
            //    .WithCreatedAtUtcTime(timeOnly.ToUtcTimeOnly())
            //    .ToListAsync(cancellationToken);

            //// 2- With Created At UtcDate
            //var localDate1 = DateOnly.FromDateTime(DateTime.UtcNow);
            //var localDate2 = DateTime.UtcNow.ToUtcDateOnly(); // Extension Method from DateTimeExtensions
            //var localDate3 = new DateOnly(2025, 03, 09);

            //var categories = await categoryRepository
            //    .WithCreatedAtUtcDate(localDate3)
            //    .ToListAsync(cancellationToken);

            // En Example Category Repository Filter ----------------------------------------

            var categories = await categoryRepository.GetAllAsync(cancellationToken);
            var categoryDtos = mapper.Map<List<CategoryDto>>(categories);
            var categoriesWithUserName = new List<CategoryDto>();
            foreach (var category in categoryDtos)
            {
                categoriesWithUserName.Add(await GetCategoryDtoWithUserNameAsync(category));
            }
            return categoriesWithUserName;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<OperationResult> CreateAsync(UpsertCategoryDto upsertCategoryDto, CancellationToken cancellationToken)
    {
        try
        {
            var categoryExists = await CategoryNameExixtsAsync(upsertCategoryDto.Name, cancellationToken);
            if (!categoryExists)
            {
                var newCategory = mapper.Map<Category>(upsertCategoryDto);
                await categoryRepository.CreateAsync(newCategory, cancellationToken);
                return new OperationResult(true, null);
            }
            return new OperationResult(false, $"Category '{upsertCategoryDto.Name}' already exists.");
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<OperationResult> UpdateAsync(UpsertCategoryDto upsertCategoryDto, CancellationToken cancellationToken)
    {
        try
        {
            var existingCategory = await GetByIdAsync(upsertCategoryDto.Id, cancellationToken);
            if (existingCategory == null)
                return new OperationResult(false, "Category not found.");

            var categoryExists = await CategoryNameExixtsAsync(upsertCategoryDto.Name, cancellationToken);
            if (categoryExists && existingCategory.Name != upsertCategoryDto.Name)
                return new OperationResult(false, $"Category '{upsertCategoryDto.Name}' already exists.");

            mapper.Map(upsertCategoryDto, existingCategory);
            await categoryRepository.UpdateAsync(existingCategory, cancellationToken);
            return new OperationResult(true, null);

        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<OperationResult> DeleteAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        try
        {
            var existingCategory = await GetByIdAsync(categoryId, cancellationToken);
            if (existingCategory != null)
            {
                await categoryRepository.DeleteAsync(existingCategory, cancellationToken);
                return new OperationResult(true, null);
            }
            return new OperationResult(false, "Category not found.");
        }
        catch (Exception)
        {
            throw;
        }
    }

    public UpsertCategoryDto MapToUpsertCategoryDto(CategoryDto categoryDto)
    {
        return mapper.Map<UpsertCategoryDto>(categoryDto);
    }

    private async Task<CategoryDto> GetCategoryDtoWithUserNameAsync(CategoryDto categoryDto)
    {
        categoryDto.CreatedByUserName = await GetUserNameByIdAsync(categoryDto.CreatedByUserName) ?? categoryDto.CreatedByUserName;
        categoryDto.ModifiedByUserName = await GetUserNameByIdAsync(categoryDto.ModifiedByUserName) ?? categoryDto.ModifiedByUserName;
        return categoryDto;
    }

    private async Task<Category?> GetByIdAsync(Guid categoryId, CancellationToken cancellationToken) =>
      await categoryRepository.GetByIdAsync(categoryId, cancellationToken);

    private async Task<bool> CategoryNameExixtsAsync(string categoryName, CancellationToken cancellationToken) =>
        await categoryRepository.ExistAsync(categoryName, cancellationToken);

    private async Task<string?> GetUserNameByIdAsync(string userId) =>
           await authenticationRepository.GetUserNemeByUserIdAsync(userId);
}