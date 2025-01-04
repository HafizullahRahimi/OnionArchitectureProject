using AutoMapper;
using OnionArchitectureProject.Application.Admin.CategoryService;
using OnionArchitectureProject.Application.Admin.CategoryService.Models;
using OnionArchitectureProject.Application.Admin.CategoryService.Models.UpsertCategoryDto;
using OnionArchitectureProject.Domain.Categories;
using OnionArchitectureProject.Application.Admin.CategoryService.Profiles;
using OnionArchitectureProject.Domain.IServices;

namespace OnionArchitectureProject.Application.Admin.CategoryService;
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository categoryRepository;
    private readonly IMapper mapper;
    private readonly IUserServiceBase authenticationService;

    public CategoryService(ICategoryRepository categoryRepository, IUserServiceBase authenticationService)
    {
        var mapperConfig = new MapperConfiguration(m =>
        {
            m.AddProfile<CategoryProfile>();
        });
        mapper = mapperConfig.CreateMapper();

        this.categoryRepository = categoryRepository;
        this.authenticationService = authenticationService;
    }

    public async Task<Guid> CreateAsync(UpsertCategoryDto categoryDto, CancellationToken cancellationToken)
    {
        var category = mapper.Map<Category>(categoryDto);
        category = await categoryRepository.CreateAsync(category, cancellationToken);
        return category.Id;
    }

    public Task DeleteAsync(Guid categoryId, CancellationToken cancellationToken) =>
        categoryRepository.DeleteAsync(categoryId, cancellationToken);

    public Task<bool> ExistAsync(Guid categoryId, CancellationToken cancellationToken) =>
        categoryRepository.ExistAsync(categoryId, cancellationToken);

    public async Task<UpsertCategoryDto?> GetByIdAsync(Guid categoryId, CancellationToken cancellationToken) =>
        mapper.Map<UpsertCategoryDto>(await categoryRepository.GetByIdAsync(categoryId, cancellationToken));

    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync(CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetAllAsync(cancellationToken);
        var categoriesWithUserName = new List<CategoryDto>();
        foreach (var category in categories)
        {
            var categoryDto = await MapToCategoryDtoAsync(category);
            categoriesWithUserName.Add(categoryDto);
        }
        return categoriesWithUserName;
    }

    public async Task<bool> UpdateAsync(UpsertCategoryDto category, CancellationToken cancellationToken)
    {
        var existingCategory = await categoryRepository.GetByIdAsync(category.Id, cancellationToken);

        if (existingCategory != null)
        {
            mapper.Map(category, existingCategory);
            await categoryRepository.UpdateAsync(existingCategory, cancellationToken);
            return true;
        }
        return false;
    }

    public UpsertCategoryDto MapToUpsertCategoryDto(CategoryDto categoryDto)
    {
        return mapper.Map<UpsertCategoryDto>(categoryDto);
    }

    private async Task<CategoryDto> MapToCategoryDtoAsync(Category category)
    {
        var categoryDto = mapper.Map<CategoryDto>(category);
        var createdByUserName = await GetUserNameAsync(category.CreatedBy);
        if (createdByUserName != null)
        {
            categoryDto.CreatedByUserName = createdByUserName;
        }
        if (!string.IsNullOrEmpty(category.ModifiedBy))
        {
            categoryDto.ModifiedByUserName = await GetUserNameAsync(category.ModifiedBy);
        }
        return categoryDto;
    }

    private async Task<string?> GetUserNameAsync(string userId) =>
        await authenticationService.GetUserNemeByIdAsync(userId);
}