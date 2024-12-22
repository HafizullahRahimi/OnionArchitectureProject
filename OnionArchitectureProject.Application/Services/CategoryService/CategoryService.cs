using AutoMapper;
using OnionArchitectureProject.Domain.Categories;
using OnionArchitectureProject.Application.Profiles;
using OnionArchitectureProject.Application.Services.CategoryService.Models.CategoryDto;
using OnionArchitectureProject.Application.Services.ProductService.Models;
using OnionArchitectureProject.Domain.Products;

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

    public async Task<Guid> CreateAsync(CategoryDto categoryDto, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Category>(categoryDto);
        category = await _categoryRepository.CreateAsync(category, cancellationToken);
        return category.Id;
    }

    public Task DeleteAsync(Guid categoryId, CancellationToken cancellationToken) =>
        _categoryRepository.DeleteAsync(categoryId, cancellationToken);

    public Task<bool> ExistAsync(Guid categoryId, CancellationToken cancellationToken) =>
        _categoryRepository.ExistAsync(categoryId, cancellationToken);

    public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAllAsync(cancellationToken);
        return categories;
    }

    public async Task<CategoryDto?> GetByIdAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId, cancellationToken);
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<bool> UpdateAsync(Guid categoryId, CategoryDto categoryDto, CancellationToken cancellationToken)
    {
        var existingCategory = await _categoryRepository.GetByIdAsync(categoryId, cancellationToken);

        if (existingCategory != null)
        {
            _mapper.Map(categoryDto, existingCategory);
            await _categoryRepository.UpdateAsync(existingCategory, cancellationToken);
            return true;
        }
        return false;
    }

    public async Task<bool> UpdateAsync(Category category, CancellationToken cancellationToken)
    {
        var existingCategory = await _categoryRepository.GetByIdAsync(category.Id, cancellationToken);

        if (existingCategory != null)
        {
            _mapper.Map(category, existingCategory);
            await _categoryRepository.UpdateAsync(existingCategory, cancellationToken);
            return true;
        }
        return false;
    }
}