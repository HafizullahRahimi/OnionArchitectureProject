using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnionArchitecture.Domain.Categories;
using OnionArchitectureProject.Api.Dto;
using OnionArchitectureProject.Application.Services.CategoryService;
using OnionArchitectureProject.Application.Services.CategoryService.Models;
using OnionArchitectureProject.Persistence;

namespace OnionArchitectureProject.Api.Controllers;
[Route("api/category")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
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

        if (category is null) return NotFound();
        return Ok(category);
    }

    // POST api/category
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] string categoryName)
    {
        var categoryId = await _categoryService.CreateAsync(categoryName, CancellationToken.None);
        return Ok(categoryId);
    }

    // PUT api/category/5
    [HttpPut("{categoryId}")]
    public async Task<IActionResult> Put(Guid categoryId, [FromBody] string categoryName)
    {
        var existCategory = await _categoryService.ExistAsync(categoryId, CancellationToken.None);

        if (!existCategory) return NotFound();

        await _categoryService.UpdateAsync(categoryId, categoryName, CancellationToken.None);

        return NoContent();
    }

    // DELETE api/category/5
    [HttpDelete("{categoryId}")]
    public async Task<IActionResult> Delete(Guid categoryId)
    {
        var existCategory = await _categoryService.ExistAsync(categoryId, CancellationToken.None);

        if (!existCategory) return BadRequest();
        await _categoryService.DeleteAsync(categoryId,CancellationToken.None);

        return Ok();
    }
}
