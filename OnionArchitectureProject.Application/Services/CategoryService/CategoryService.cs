using AutoMapper;
using OnionArchitectureProject.Application.Profiles;
using OnionArchitectureProject.Application.Services.CategoryService.Models;
using OnionArchitectureProject.Application.Services.CategoryService.Models.UpsertCategoryDto;
using OnionArchitectureProject.Domain.AuthenticationService;
using OnionArchitectureProject.Domain.Categories;

namespace OnionArchitectureProject.Application.Services.CategoryService;
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository categoryRepository;
    private readonly IMapper mapper;
    private readonly IAuthenticationService authenticationService;

    public CategoryService(ICategoryRepository categoryRepository, IAuthenticationService authenticationService)
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
            var categoryDto = await MapCategoryToDtoAsync(category);
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

    private async Task<CategoryDto> MapCategoryToDtoAsync(Category category)
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