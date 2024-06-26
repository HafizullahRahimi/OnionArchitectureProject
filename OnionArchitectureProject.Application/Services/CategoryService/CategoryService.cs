using AutoMapper;
using OnionArchitectureProject.Domain.Categories;
using OnionArchitectureProject.Application.Profiles;
using OnionArchitectureProject.Application.Services.CategoryService.Models;

namespace OnionArchitectureProject.Application.Services.CategoryService;
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;

        var mapperConfig = new MapperConfiguration(m =>
        {
            m.AddProfile<CategoryProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    public async Task<Guid> CreateAsync(string categoryName, string createdBy, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Name = categoryName,
            CreatedBy = createdBy,
        };
        category = await _categoryRepository.CreateAsync(category, cancellationToken);
        return category.Id;
    }

    public Task DeleteAsync(Guid categoryId, CancellationToken cancellationToken) =>
        _categoryRepository.DeleteAsync(categoryId, cancellationToken);

    public Task<bool> ExistAsync(Guid categoryId, CancellationToken cancellationToken) =>
        _categoryRepository.ExistAsync(categoryId, cancellationToken);

    public async Task<IEnumerable<CategoryDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAllAsync(cancellationToken);
        var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
        return categoryDtos;
    }

    public async Task<CategoryDto?> GetByIdAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId, cancellationToken);
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task UpdateAsync(Guid categoryId, string categoryName, string modifiedBy, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId, cancellationToken);
        category.Name = categoryName;
        category.ModifiedBy = modifiedBy;

        await _categoryRepository.UpdateAsync(category, cancellationToken);
    }
}