using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnionArchitectureProject.Application.Services.CategoryService;
using OnionArchitectureProject.Application.Services.CategoryService.Models;
using OnionArchitectureProject.Authentication.Services.TokenService;

namespace OnionArchitectureProject.Api.Controllers;

[Authorize]
[Route("api/category")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly ITokenService _tokenService;

    public CategoryController(ICategoryService categoryService, ITokenService tokenService)
    {
        _categoryService = categoryService;
        _tokenService = tokenService;
    }

    // GET api/category
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> Get()
    {
        var categoryDtos = await _categoryService.GetAllAsync(CancellationToken.None);
        return Ok(categoryDtos);
    }

    // GET api/category/5
    [HttpGet("{categoryId}")]
    public async Task<ActionResult<CategoryDto>> Get(Guid categoryId)
    {
        var category = await _categoryService.GetByIdAsync(categoryId, CancellationToken.None);

        if (category is null) return NotFound("Category not found");
        return Ok(category);
    }

    // POST api/category
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] string categoryName)
    {
        var userName = _tokenService.GetCurrentUserName();
        var categoryId = await _categoryService.CreateAsync(categoryName, userName, CancellationToken.None);
        return Ok($"Added category with id: {categoryId}");
    }

    // PUT api/category/5
    [HttpPut("{categoryId}")]
    public async Task<IActionResult> Put(Guid categoryId, [FromBody] string categoryName)
    {
        var existCategory = await _categoryService.ExistAsync(categoryId, CancellationToken.None);
        if (!existCategory) return NotFound("Category not found");

        var userName = _tokenService.GetCurrentUserName();

        await _categoryService.UpdateAsync(categoryId, categoryName, userName, CancellationToken.None);
        return Ok($"Updated category with id: {categoryId}");
    }

    // DELETE api/category/5
    [HttpDelete("{categoryId}")]
    public async Task<IActionResult> Delete(Guid categoryId)
    {
        var existCategory = await _categoryService.ExistAsync(categoryId, CancellationToken.None);
        if (!existCategory) return NotFound("Category not found");

        await _categoryService.DeleteAsync(categoryId, CancellationToken.None);
        return Ok("Category deleted");
    }
}
