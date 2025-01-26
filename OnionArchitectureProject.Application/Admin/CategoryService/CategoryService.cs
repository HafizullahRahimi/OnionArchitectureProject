using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OnionArchitectureProject.Application.Admin.CategoryService.Models;
using OnionArchitectureProject.Application.Admin.CategoryService.Models.UpsertCategoryDto;
using OnionArchitectureProject.Authentication.Models;
using OnionArchitectureProject.Domain.Categories;

namespace OnionArchitectureProject.Application.Admin.CategoryService;
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository categoryRepository;
    private readonly IMapper mapper;
    private readonly UserManager<ApplicationUser> userManager;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        this.categoryRepository = categoryRepository;
        this.mapper = mapper;
        this.userManager = userManager;
    }

    public async Task<List<CategoryDto>?> GetCategoriesAsync(CancellationToken cancellationToken)
    {
        try
        {
            var categories = await categoryRepository.GetAllAsync(cancellationToken);
            var categoryDtos = mapper.Map<List<CategoryDto>>(categories);
            if (categoryDtos == null || categoryDtos.Count == 0)
                return categoryDtos;
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

    public async Task<bool> CreateAsync(UpsertCategoryDto upsertCategoryDto, CancellationToken cancellationToken)
    {
        try
        {
            if (upsertCategoryDto == null)
                return false;
            var newCategory = mapper.Map<Category>(upsertCategoryDto);
            newCategory = await categoryRepository.CreateAsync(newCategory, cancellationToken);
            return newCategory != null;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> UpdateAsync(UpsertCategoryDto upsertCategoryDto, CancellationToken cancellationToken)
    {
        try
        {
            var foundCategory = await GetByIdAsync(upsertCategoryDto.Id, cancellationToken);
            if (foundCategory != null)
            {
                mapper.Map(upsertCategoryDto, foundCategory);
                await categoryRepository.UpdateAsync(foundCategory, cancellationToken);
                return true;
            }
            return false;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> DeleteAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        try
        {
            var existingCategory = await ExistAsync(categoryId, cancellationToken);
            if (existingCategory)
            {
                await categoryRepository.DeleteAsync(categoryId, cancellationToken);
                return true;
            }
            return false;
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

    private async Task<Category?> GetByIdAsync(Guid categoryId, CancellationToken cancellationToken) =>
      await categoryRepository.GetByIdAsync(categoryId, cancellationToken);

    private async Task<bool> ExistAsync(Guid categoryId, CancellationToken cancellationToken) =>
       await categoryRepository.ExistAsync(categoryId, cancellationToken);

    private async Task<CategoryDto> GetCategoryDtoWithUserNameAsync(CategoryDto categoryDto)
    {
        var createdByUserName = await GetUserNameByIdAsync(categoryDto.CreatedByUserName);
        if (createdByUserName != null)
        {
            categoryDto.CreatedByUserName = createdByUserName;
        }

        var modifiedByUserName = await GetUserNameByIdAsync(categoryDto.ModifiedByUserName);

        if (modifiedByUserName != null)
        {
            categoryDto.ModifiedByUserName = modifiedByUserName;
        }
        return categoryDto;
    }

    private async Task<string?> GetUserNameByIdAsync(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return null;
        var user = await userManager.FindByIdAsync(userId);
        return user?.UserName ?? null;
    }
}